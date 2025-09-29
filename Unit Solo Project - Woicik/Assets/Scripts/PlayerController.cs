using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Vector3 respawnPoint;
    Camera playerCam;
    Ray ray;
    RaycastHit hit;

    Rigidbody rb;

    float verticalMove;
    float horizontalMove;

    public float speed = 5f;
    public float jumpHeight = 10;
    public float groundDetectLength = .5f;

    public int health = 5;
    public int maxHealth = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ray = new Ray(transform.position, transform.forward);
        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Camera Rotation System
        Quaternion playerRotation = playerCam.transform.rotation;
        playerRotation.x = 0;
        playerRotation.z = 0;
        transform.rotation = playerRotation;
       

        // Movement System
        Vector3 temp = rb.linearVelocity;

        temp.x = verticalMove * speed;
        temp.z = horizontalMove * speed;

        ray.origin = transform.position;
        ray.direction = -transform.up;


        rb.linearVelocity = (temp.x * transform.forward) + (temp.y * transform.up) + (temp.z * transform.right);
    }


    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputAxis = context.ReadValue<Vector2>();

        verticalMove = inputAxis.y;
        horizontalMove = inputAxis.x;
    }

    public void Jump()
    {
        if (Physics.Raycast(ray, groundDetectLength))
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "killzone")
            health = 0;

        if ((other.tag == "health") && (health < maxHealth))
        {
            health++;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "hazard")
            health--;
    }
}
