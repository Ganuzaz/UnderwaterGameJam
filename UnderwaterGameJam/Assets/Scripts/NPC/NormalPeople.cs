using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPeople : People
{

    private Rigidbody2D rigidbody;

    public Vector2 idleTimerRange = new Vector2(0.0f, 3.0f);
    public Vector2 walkingTimerRange = new Vector2(1.0f, 4.0f);

    public float walkingSpeed = 1.5f;
    public float runningSpeed = 3.0f;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        timer = new Timer(this);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void StopAndStartTimer(Vector2 range,BehaviorState state)
    {
        timer.StopTimerAndRemoveListeners();
        timer.SetTimeLimit(Random.Range(range.x, range.y));
        timer.AddListener(() => { ChangeState(state); });
    }

    protected override void OnChangeToConfused()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnChangeToFalling()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnChangeToIdle()
    {
        StopAndStartTimer(idleTimerRange, BehaviorState.WALKING);
    }

    protected override void OnChangeToNoticed()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnChangeToSlipping()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnChangeToWalking()
    {
        StopAndStartTimer(walkingTimerRange, BehaviorState.IDLE);
        walkingSpeed = Random.Range(0, 2) == 0 ?  -Mathf.Abs(walkingSpeed) : Mathf.Abs(walkingSpeed);
    }

    protected override void OnConfused()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnFalling()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnIdle()
    {
    }

    protected override void OnNoticed()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnSlipping()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnWalking()
    {
        throw new System.NotImplementedException();
    }

 


}
