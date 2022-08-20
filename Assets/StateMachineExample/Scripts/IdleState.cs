using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{

    public float IdleTimer;
    public float _waitTimer;

    public override void InitState()
    {
        base.InitState();
        _waitTimer = IdleTimer;

        Debug.Log("New State Patrol");
    }

    public override void Execute()
    {
        _waitTimer -= Time.deltaTime;
        if (_waitTimer < 0f)
        {
            Context.ChangeState(Context.Patrol);
        }

    }


}
