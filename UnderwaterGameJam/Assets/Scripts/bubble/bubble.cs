using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        NormalPeople NP = hitInfo.GetComponent<NormalPeople>();
        if(NP != null)
        {
            Debug.Log("Kena Balon");
            NP.Bubbled(hitInfo);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
