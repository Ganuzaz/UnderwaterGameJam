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

    protected void ChangeState(BehaviorState state)
    {
        switch (state) {

            case BehaviorState.IDLE:
                OnChangeToIdle();
                break;
            case BehaviorState.WALKING:

                break;
            case BehaviorState.CONFUSED:
                break;
            case BehaviorState.NOTICED:
                break;
            case BehaviorState.SLIPPING:
                break;
            case BehaviorState.FALLING:
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
    protected abstract void OnIdle();
    protected abstract void OnWalking();
    protected abstract void OnConfused();
    protected abstract void OnNoticed();
    protected abstract void OnSlipping();
    protected abstract void OnFalling();


    protected abstract void OnChangeToIdle();
    protected abstract void OnChangeToWalking();
    protected abstract void OnChangeToConfused();
    protected abstract void OnChangeToNoticed();
    protected abstract void OnChangeToSlipping();
    protected abstract void OnChangeToFalling();


    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
