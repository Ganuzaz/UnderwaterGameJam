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
        
            hitInfo.transform.position = transform.position;
            hitInfo.transform.parent = this.transform;
            StartCoroutine(bubbling(hitInfo.transform));
        }
    }


    IEnumerator bubbling(Transform ppl){
        while(true){
            ppl.transform.position = this.transform.position;
            yield return null;
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
