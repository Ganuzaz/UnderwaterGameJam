using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleWithGun : NormalPeople {
    // Start is called before the first frame update
    private Rigidbody2D rigidbody;
    public GameObject GunHand;
    public bool EnemyNoticed = false;

    private void Awake()
    {
        rigidbody =  GetComponent<Rigidbody2D>();
    }

    private void StopAndStartTimer(Vector2 range, BehaviorState state)
    {
        timer.StopTimerAndRemoveListeners();
        timer.SetTimeLimit(Random.Range(range.x, range.y));
        timer.AddListener(() => { ChangeState(state); });
        timer.StartTimer();
        //Debug.Log("Listener Added");
    }

    protected override void OnChangeToWalking()
    {
        if (EnemyNoticed)
        {
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

    }

    protected override void OnChangeToNoticed()
    {
        GunHand.transform.rotation.z.Equals(30);
        StopAndStartTimer(walkingTimerRange, BehaviorState.WALKING);
        //animation nembak
        //if udh luar kamera ancurin
    }





}
