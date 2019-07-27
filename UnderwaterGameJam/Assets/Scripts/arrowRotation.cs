using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowRotation : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject monster;
    //public Sprite newArrow,oldArrow;
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
    }
    // Update is called once per frame
    void Update()
    {
         
        if (monster.GetComponent<Transform>().localScale.x < 0) {
            //this.GetComponent<SpriteRenderer>().sprite = newArrow;

            //this is used to flip the arrow because the way the code works tends to create a buggy rotation depending on where the arrow is pointing initially
            //sr.flipX = true; sr.flipY = true;

            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(-180 + angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        else if(monster.GetComponent<Transform>().localScale.x > 0) {
            //this.GetComponent<SpriteRenderer>().sprite = oldArrow;
           
            

            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
