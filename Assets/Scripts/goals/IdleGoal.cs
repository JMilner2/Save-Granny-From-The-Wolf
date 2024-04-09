using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGoal : BaseGoalScript
{
    [SerializeField] int priority = 10;
    public override int OnCalculatePriority() //only idle when priority 10 is highest. (mostly used to guard keys at game start)
    {
        
        return priority;
    }

    public override bool CanRun()
    {
        return true;
    }
}
