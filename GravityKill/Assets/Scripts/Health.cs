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
            deathScreen();
        }
    }

    private void respawn()
    {
        // on pourrait par exemple faire un pool de respawnpoint dans une ArrayList et faire un Random.Range tavu
        //transform.position = this.GetComponent<CharacterManagerCh>().respawnPos;
    }

    private void deathScreen()
    {
        // faire un truc de daube kan tu crèves avec des particules xd genre passer en 3rd person
        respawn();
    }
}
