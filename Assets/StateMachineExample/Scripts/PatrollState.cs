using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollState : BaseState
{
    public List<Transform> PatrolPoints;
    public int PatrolIndex;
    public float moveSpeed;

    public override void InitState()
    {
        base.InitState();

        PatrolIndex = 0;
        Debug.Log("New State Patrol");
    }

    public override void Execute()
    {
        if (PatrolIndex == PatrolPoints.Count)
        {
            Context.ChangeState(Context.Idle);
            return;
        }

        Vector3 targetPos = PatrolPoints[PatrolIndex].position;
        Debug.Log("Patrol Index: " + PatrolIndex + " distance left: " + Vector3.Distance(transform.position, targetPos));
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            PatrolIndex += 1;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }
}
