using UnityEngine;
using UnityEngine.AI;
public class BasicEnemyController : MonoBehaviour
{
    PlayerController player;

    [Header("Logic")]
    NavMeshAgent agent;
    public bool isFollowing = false;

    [Header("Enemy Stats")]
    public int health = 5;
    public int maxHealth = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        else
        {
            if (isFollowing)
                agent.destination = player.transform.position;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "proj")
        {
            health--;
            Destroy(collision.gameObject);
        }
    }
}
