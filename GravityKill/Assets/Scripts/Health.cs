using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// on pourrait le foutre dans le CharacterManager mais bon
public class Health : MonoBehaviour {

    public const int maxHealth = 100;
    public int hp = maxHealth;

    public void takeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            hp = maxHealth;
            respawn();
        }
    }

    public void respawn()
    {
        //transform.position = this.GetComponent<CharacterManagerCh>().respawnPos;
    }
}
