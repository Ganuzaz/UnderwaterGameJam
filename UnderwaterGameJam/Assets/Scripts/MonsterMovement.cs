using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{

    public float speed, headButtPower;
    private float monsterRotationSpeed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveVelocity;
    public GameObject arrow, water;

    private bool headbutting = false, readyAim = false, canMove = true;
    SpriteRenderer sr;
    // Start is called before the first frame update

    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Velocity = " + rb.velocity);
        moveVelocity = Vector2.zero;
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


        if (moveVelocity.magnitude == 0)
        {
            var currentState = anim.GetCurrentAnimatorStateInfo(0);
            Debug.Log(currentState.fullPathHash);
            Debug.Log(Animator.StringToHash("Charge"));
            if (currentState.fullPathHash == Animator.StringToHash("Base Layer.Swim"))
            {
                Debug.Log("I'm swimming");
                anim.speed = 0;
            }
        }
        else
        {
            anim.speed = 1;
        }

        if ((Input.GetButtonDown("Fire1") || readyAim == true) && !headbutting)
        {
            headButtAim();
        }


    }

    private void FixedUpdate()
    {
        if (canMove == true)
        {          

            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);


        }

        headButtForce();




    }
    void headButtAim()  //the aiming of the headbutt
    {
        anim.SetBool("charge", true);
        rb.gravityScale = 0;
        canMove = false;
        //tell public bool in shootBubble script to unable shooting
        GetComponent<shootBubble>().canShoot = false;
        readyAim = true;
        arrow.GetComponent<SpriteRenderer>().enabled = true;

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        var temp = transform.localScale;
        temp.x = direction.x > transform.position.x ? 1 : -1;
        transform.localScale = temp;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle = this.GetComponent<Transform>().localScale.x < 0 ? -180 + angle : angle;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (this.GetComponent<Transform>().localScale.x > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, monsterRotationSpeed * Time.deltaTime);
        }
        else if (this.GetComponent<Transform>().localScale.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, monsterRotationSpeed * Time.deltaTime);
        }


    }

    void headButtForce()  //the actual headbutt
    {


        if (Input.GetButtonUp("Fire1") && readyAim)
        {
            anim.SetBool("charge", false);
            
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var mouseDir = mousePos - gameObject.transform.position;
            mouseDir.z = 0.0f;
            mouseDir = mouseDir.normalized;
            arrow.GetComponent<SpriteRenderer>().enabled = false;
            rb.gravityScale = 3;
            rb.AddForce(mouseDir * headButtPower);
            readyAim = false;
            headbutting = true;
            StartCoroutine(checkHeadbuttStatus());
        }
    }

    IEnumerator checkHeadbuttStatus()
    {

        Debug.Log(rb.velocity);
        yield return null;
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit detected");
        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        canMove = true;
    }


}
