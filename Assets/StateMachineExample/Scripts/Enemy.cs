using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public IdleState Idle;
    public PatrollState Patrol;

    public BaseState ActiveState;

    public void ChangeState(BaseState state)
    {
        Debug.Log("State Changed");
        ActiveState.StopState();

        ActiveState = state;
        ActiveState.InitState();
    }

    private void Update()
    {
        ActiveState.Execute();
    }

    private void Start()
    {
        ActiveState = Idle;
        ActiveState.InitState();
    }

}
