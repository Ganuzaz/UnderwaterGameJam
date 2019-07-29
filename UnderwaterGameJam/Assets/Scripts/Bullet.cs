using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20f;
    public int damage = 1;
    public Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody.velocity = transform.right * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterHealth MH = collision.GetComponent<MonsterHealth>();
        if(MH != null)
        {
            MH.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
