using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleWithGun : NormalPeople {
    // Start is called before the first frame update
    private Rigidbody2D rigidbody;
    public GameObject GunHand;
    public bool EnemyNoticed = false;

    public Transform FirePoint;
    public GameObject BulletPrefab;

    private void Awake()
    {
        rigidbody =  GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }

    private void StopAndStartTimer(Vector2 range, BehaviorState state)
    {
        timer.StopTimerAndRemoveListeners();
        timer.SetTimeLimit(Random.Range(range.x, range.y));
        timer.AddListener(() => { ChangeState(state); });
        timer.StartTimer();
        //Debug.Log("Listener Added");
    }

    protected override void OnChangeToIdle()
    {
        anim.SetBool("IsWalking", false);
        rigidbody.velocity = Vector2.zero;
        StopAndStartTimer(idleTimerRange, BehaviorState.WALKING);
    }
       

    protected override void OnChangeToWalking()
    { 
        if (EnemyNoticed)
        {
            GunHand.transform.rotation = Quaternion.Euler(0, 0, 0);
            StopAndStartTimer(walkingTimerRange, BehaviorState.NOTICED);
            walkingSpeed = Random.Range(0, 2) == 0 ? -Mathf.Abs(walkingSpeed) : Mathf.Abs(walkingSpeed);
            rigidbody.velocity = new Vector2(walkingSpeed, 0);
            //animation shooting
        }
        else
        {
            StopAndStartTimer(walkingTimerRange, BehaviorState.IDLE);
            walkingSpeed = Random.Range(0, 2) == 0 ? -Mathf.Abs(walkingSpeed) : Mathf.Abs(walkingSpeed);
            rigidbody.velocity = new Vector2(walkingSpeed, 0);
            //animation walking
        }

        anim.SetBool("IsWalking", true);


    }

    protected override void OnChangeToNoticed()
    {
        GunHand.transform.rotation = Quaternion.Euler(0, 0, 30);
        Shoot();
        StopAndStartTimer(walkingTimerRange, BehaviorState.WALKING);
        //animation nembak
        //if udh luar kamera ancurin
    }

    private void Shoot()
    {
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
    }





}
