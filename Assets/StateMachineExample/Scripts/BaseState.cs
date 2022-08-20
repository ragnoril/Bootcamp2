using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{

    public Enemy Context;

    public bool IsStateActive;

    public virtual void InitState()
    {
        IsStateActive = true;
    }

    public virtual void StopState()
    {
        IsStateActive = false;
    }

    public virtual void Execute()
    {
        
    }

}
