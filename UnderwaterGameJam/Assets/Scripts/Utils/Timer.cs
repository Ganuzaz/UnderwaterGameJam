using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer 
{
    public delegate void OnTimerFinishedEvent();
    public event OnTimerFinishedEvent OnTimerFinished;
    private float timeLimit;
    private float timePassed=0;
    private bool coroutineRunning = false;
    private IEnumerator runningCoroutine;
    private MonoBehaviour owner;

    public Timer(MonoBehaviour owner)
    {
        this.owner = owner;
    }

    public void StartTimer()
    {
        if (coroutineRunning)
        {
            owner.StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartTimerCoroutine();        
        timePassed = 0;
        owner.StartCoroutine(StartTimerCoroutine());
    }

    public void StopTimer()
    {
        if (coroutineRunning)
        {
            owner.StopCoroutine(runningCoroutine);
        }
        coroutineRunning = false;
    }
    public void StopTimerAndRemoveListeners()
    {
        StopTimer();
        RemoveAllListener();
    }

    public void SetTimeLimit(float limit)
    {
        timeLimit = limit;
    }


    public void AddListener(OnTimerFinishedEvent listener)
    {
        OnTimerFinished += listener;
    }

    public void RemoveAllListener()
    {
        if(OnTimerFinished!=null)
        foreach(Delegate d in OnTimerFinished.GetInvocationList())
        {
            OnTimerFinished -= (OnTimerFinishedEvent)d;
        }
    }

    IEnumerator StartTimerCoroutine()
    {
        Debug.Log("Coroutine Start");
        coroutineRunning = true;
        while (timePassed <= timeLimit)
        {
            //Debug.Log("timer = "+timePassed);
            timePassed += Time.deltaTime;
            yield return null;
        }

        OnTimerFinished?.Invoke();
        coroutineRunning = false;        
    }
   
}
