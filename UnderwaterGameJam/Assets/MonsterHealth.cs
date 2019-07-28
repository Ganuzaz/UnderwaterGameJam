using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int Health = 1;

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if(Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        //load game over screen
    }
}
