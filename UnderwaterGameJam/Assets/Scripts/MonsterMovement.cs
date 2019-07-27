using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float speed;
    private float monsterRotationSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    public GameObject arrow;
    private bool readyHeadbutt = false, canMove= true;
    SpriteRenderer sr;
    // Start is called before the first frame update

    void Start()
    {
        
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput.normalized * speed;
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                transform.localScale = new Vector2(1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                
                transform.localScale = new Vector2(-1f, 1f);
            }
        }
       



            if (Input.GetButtonDown("Fire1") || readyHeadbutt== true)
            {
                headButt();
            }
    }

    private void FixedUpdate()
    {
        if (canMove == true)
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        }
    }
    void headButt()
    {
        canMove = false;
        //tell public bool in shootBubble script to unable shooting
        GetComponent<shootBubble>().canShoot = false;
        readyHeadbutt = true;
        arrow.SetActive(true);
        if (this.GetComponent<Transform>().localScale.x > 0) { 
            
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, monsterRotationSpeed * Time.deltaTime);
            
            
        }
        else if (this.GetComponent<Transform>().localScale.x < 0) {
            //sr.flipX = true; sr.flipY = true;

            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(-180 + angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, monsterRotationSpeed* Time.deltaTime);
         }
    }
}
