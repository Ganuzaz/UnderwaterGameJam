using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class People : MonoBehaviour
{
    protected enum BehaviorState
    {
        IDLE,
        WALKING,
        CONFUSED,
        NOTICED,
        SLIPPING,
        FALLING
    }
    protected BehaviorState currentBehavior;
    public Vector2 speed = new Vector2(0,0);

    protected Timer timer;
    protected void ChangeState(BehaviorState state)
    {
        switch (state) {

            case BehaviorState.IDLE:
                OnChangeToIdle();
                break;
            case BehaviorState.WALKING:
                OnChangeToWalking();
                break;
            case BehaviorState.CONFUSED:
                OnChangeToConfused();
                break;
            case BehaviorState.NOTICED:
                OnChangeToNoticed();
                break;
            case BehaviorState.SLIPPING:
                OnChangeToSlipping();
                break;
            case BehaviorState.FALLING:
                OnChangeToFalling();
                break;
        }

    }

    protected void OnState()
    {
        switch (currentBehavior)
        {
            case BehaviorState.IDLE:
                OnIdle();
                break;
            case BehaviorState.WALKING:
                OnWalking();
                break;
            case BehaviorState.CONFUSED:
                OnConfused();
                break;
            case BehaviorState.NOTICED:
                OnNoticed();
                break; 
            case BehaviorState.SLIPPING:
                OnSlipping();
                break;
            case BehaviorState.FALLING:
                OnFalling();
                break;
        }

    }
    #region StateUpdate
    protected abstract void OnIdle();
    protected abstract void OnWalking();
    protected abstract void OnConfused();
    protected abstract void OnNoticed();
    protected abstract void OnSlipping();
    protected abstract void OnFalling();
    #endregion

    #region OnChangeState
    protected abstract void OnChangeToIdle();
    protected abstract void OnChangeToWalking();
    protected abstract void OnChangeToConfused();
    protected abstract void OnChangeToNoticed();
    protected abstract void OnChangeToSlipping();
    protected abstract void OnChangeToFalling();
    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentBehavior = BehaviorState.IDLE;
    }

    // Update is called once per frame
    protected virtual void Update()
    {   
        OnState();
    }
}
