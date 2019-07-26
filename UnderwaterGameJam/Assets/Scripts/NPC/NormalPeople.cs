using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPeople : People
{

    public Vector2 idleTimerRange = new Vector2(0, 3);

    

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
