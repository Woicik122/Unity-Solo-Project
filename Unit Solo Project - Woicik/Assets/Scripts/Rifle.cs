using UnityEngine;

public class Rifle : Weapon
{
    public void changeFireMode()
    {
        if (fireModes > 0)
        {
            currentFireMode++;

            if (currentFireMode >= fireModes)
                currentFireMode = 0;

            if (currentFireMode == 0)
            {
                holdToAttack = false;
                rof = 1;
            }
            else
            {
                holdToAttack = true;
                rof = .25f;
            }
        }
    }
}
