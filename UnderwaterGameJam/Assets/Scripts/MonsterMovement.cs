using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public CameraScript CameraShake;
    public float speed, headButtPower;
    private float monsterRotationSpeed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveVelocity;
    public GameObject arrow, water;
    public AudioSource headButtSound, headButtImpact,waterSound;


    public float limitxLeft, limitxRight, limityUp, limityDown;

    private bool  readyAim = false, canMove = true;
    public bool headbutting = false;
    SpriteRenderer sr;
    // Start is called before the first frame update
    public int health = 5;
    public GameObject bloodEffect;
    public bool dead = false;
    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        
        

    }

    // Update is called once per frame
    void Update()
    {
        if(dead)
        return;
        moveVelocity = Vector2.zero;
        if (canMove == true)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput.normalized * speed;

            //transform.position = new Vector3(Mathf.Clamp(transform.position.x, limitxLeft, limitxRight), Mathf.Clamp(transform.position.y, limityDown, limityUp), transform.position.z);
            

            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                transform.localScale = new Vector2(0.5f, 0.5f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                transform.localScale = new Vector2(-0.5f, 0.5f);
            }
        }


        if (moveVelocity.magnitude == 0 && canMove)
        {
            var currentState = anim.GetCurrentAnimatorStateInfo(0);
            if (currentState.fullPathHash == Animator.StringToHash("Base Layer.Swim"))
            {
                anim.speed = 0;
            }
        }
        else
        {
            if (hitGround) { anim.speed = 0;Debug.Log("ANIM SPEED 0"); }
            else if (headbutting) anim.speed = 2;
            else anim.speed = 1;
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
        readyAim = true;
        arrow.GetComponent<SpriteRenderer>().enabled = true;

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        var temp = transform.localScale;

        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x){
            temp.x = Mathf.Abs(temp.x);
        }else{
            temp.x = -Mathf.Abs(temp.x);
        }
        

        temp.x = direction.x > transform.position.x ? 0.5f : -0.5f;
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
            GetComponent<shootBubble>().canShoot = false;
            anim.SetBool("charge", false);
            headButtSound.Play();
            arrow.GetComponent<SpriteRenderer>().enabled = true;

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var mouseDir = mousePos - gameObject.transform.position;
            mouseDir.z = 0.0f;
            mouseDir = mouseDir.normalized;
            arrow.GetComponent<SpriteRenderer>().enabled = false;
            rb.gravityScale = 0;
            rb.AddForce(mouseDir * headButtPower);
            readyAim = false;
            headbutting = true;
            StartCoroutine(checkHeadbuttStatus());
        }
    }

    IEnumerator checkHeadbuttStatus()
    {
        yield return new WaitForFixedUpdate();
        while (rb.velocity.magnitude > 4)
        {
            
            yield return null;
        }
        if (hitGround)
        {
            yield return new WaitForFixedUpdate();
            while (rb.velocity.magnitude > 4)
            {
                yield return null;
            }

        }

        float time = 0;
        while (transform.rotation.eulerAngles.magnitude > 0.1f)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, time);

            if (transform.rotation.eulerAngles.magnitude <= 1)
                transform.rotation = Quaternion.identity;

            yield return null;
        }

        hitGround = false;
        canMove = true;
        headbutting = false;
        GetComponent<shootBubble>().canShoot = true;
        rb.gravityScale = 1f;
    }
    private bool hitGround = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.tag);
        if (headbutting && collision.transform.CompareTag("Ground"))
        {
            
            headButtImpact.Play();
            hitGround = true;
            //StartCoroutine(Camera.main.GetComponent<CameraScript>().Shake(0.15f, 0.4f));
        }

        if(collision.transform.name.Equals("Bullet")){
            
            health--;
            var temp = Instantiate(bloodEffect);
            temp.transform.position = this.transform.position;
            if(health<=0){
                dead = true;
                anim.SetBool("Dead",true);
            }
            
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.transform.CompareTag("Man")){
           anim.speed =1;
           anim.SetTrigger("eat");
       }
    }


}
