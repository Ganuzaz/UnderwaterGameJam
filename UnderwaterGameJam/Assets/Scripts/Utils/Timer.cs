using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer 
{
    public delegate void OnTimerFinishedEvent();
    public event OnTimerFinishedEvent OnTimerFinished;
    public float timeLimit;
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
        
        runningCoroutine = StartTimerCoroutine();
        if (coroutineRunning)
        {
            owner.StopCoroutine(runningCoroutine);
        }
        timePassed = 0;
        owner.StartCoroutine(StartTimerCoroutine());
    }

    public void AddListener(OnTimerFinishedEvent listener)
    {
        OnTimerFinished += listener;
    }

    public void RemoveAllListener()
    {
        foreach(Delegate d in OnTimerFinished.GetInvocationList())
        {
            OnTimerFinished -= (OnTimerFinishedEvent)d;
        }
    }

    IEnumerator StartTimerCoroutine()
    {
        coroutineRunning = true;
        while (timePassed <= timeLimit)
        {
            timePassed += Time.deltaTime;
            yield return null;
        }

        OnTimerFinished?.Invoke();
        coroutineRunning = false;        
    }
   
}
