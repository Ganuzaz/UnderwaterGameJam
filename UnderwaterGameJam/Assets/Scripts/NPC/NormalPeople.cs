using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPeople : People
{

    private Rigidbody2D rigidbody;
    protected Animator anim;

    public GameObject monster;

    public Vector2 idleTimerRange = new Vector2(0.0f, 3.0f);
    public Vector2 walkingTimerRange = new Vector2(1.0f, 4.0f);

    public float walkingSpeed = 1.5f;
    public float runningSpeed = 3.0f;

    private bool facingLeft = false;

    private int health = 1;

    public GameObject bloodParticle;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        timer = new Timer(this);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        LineofSight();
        //Debug.Log(GetState());
    }

    private void StopAndStartTimer(Vector2 range,BehaviorState state)
    {
        timer.StopTimerAndRemoveListeners();
        timer.SetTimeLimit(Random.Range(range.x, range.y));
        timer.AddListener(() => { ChangeState(state); });
        timer.StartTimer();
        //Debug.Log("Listener Added");
    }

    protected override void OnChangeToConfused()
    {
        throw new System.NotImplementedException();
        //ignore
    }

    protected override void OnChangeToFalling()
    {   
        timer.StopTimerAndRemoveListeners();
        rigidbody.gravityScale = 0.1f;
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Animator>().SetBool("isWalking", false);
        GetComponent<Animator>().SetBool("Dead",true);
        GetComponent<Animator>().speed =0;
        GetComponent<Animator>().SetBool("isWalking", false);
    }

    protected override void OnChangeToIdle()
    {
        rigidbody.velocity = Vector2.zero;
        StopAndStartTimer(idleTimerRange, BehaviorState.WALKING);
        //animation idle
    }

    protected override void OnChangeToNoticed()
    {
        rigidbody.velocity = new Vector2(runningSpeed, 0);
        //animation running
        //if udh luar kamera ancurin
    }

    protected override void OnChangeToSlipping()
    {
        throw new System.NotImplementedException();
        //ignore
    }

    protected override void OnChangeToWalking()
    {
                

        StopAndStartTimer(walkingTimerRange, BehaviorState.IDLE);
        walkingSpeed = Random.Range(0, 2) == 0 ?  Mathf.Abs(walkingSpeed) : Mathf.Abs(walkingSpeed);
        rigidbody.velocity = new Vector2(walkingSpeed, 0);
        
        /*if(rigidbody.velocity != new Vector2(0, 0))
        {
            anim.SetBool("IsWalking", false);
        }
        else
        {
            anim.SetBool("IsWalking", true);
        }*/

    }

    protected override void OnConfused()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnFalling()
    {
        //throw new System.NotImplementedException();
    }

    protected override void OnIdle()
    {

    }

    protected override void OnNoticed()
    {
        
    }

    protected override void OnSlipping()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnWalking()
    {

    }


    protected virtual void LineofSight()
    {
        if(GetState() == BehaviorState.FALLING)
        return;

        var hit = Physics2D.Raycast(transform.position, facingLeft ? Vector2.left : Vector2.right, 5f,LayerMask.GetMask("People"));

        if (hit)
        {
            if (hit.transform.GetComponent<People>() )
            {
                switch (hit.transform.GetComponent<People>().GetState()) {
                    case BehaviorState.FALLING:
                        ChangeState(BehaviorState.NOTICED);
                        break;
                }

            }


        }
    }

    public override int Damage()
    {
        health -= 1;
        if (health <= 0)
        {
            ChangeState(BehaviorState.FALLING);
        }
        return health;
    }

    public void Bubbled()
    {
        timer.StopTimerAndRemoveListeners();
        ChangeState(BehaviorState.FALLING);
        rigidbody.gravityScale=0;
        rigidbody.velocity = Vector3.zero;
        //gameObject.transform.parent = HitInfo.transform;           
    }

    public void OnTriggerEnter2D(Collider2D col){
        if(col.transform.CompareTag("Player")){
            GetComponent<SpriteRenderer>().enabled = false;
            var temp = Instantiate(bloodParticle);
            temp.transform.position = transform.position;
            temp.GetComponent<ParticleSystem>().Play();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
