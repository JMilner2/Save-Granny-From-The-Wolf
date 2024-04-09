using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunAwayGoal : BaseGoalScript
{
    [SerializeField] int runPriority = 100;
    int currentPriority = 0;

    public override void OnTickGoal()
    {
        currentPriority = 0;

        if (agent.runAway == false)
        {
            currentPriority = 0; 
        }

        if (agent.runAway == true)
        {
            currentPriority = runPriority;
        }
    }

    public override void OnGoalDeactivated()
    {
        base.OnGoalDeactivated();
    }


    public override int OnCalculatePriority()
    {
        return Mathf.FloorToInt(currentPriority);
    }

    public override bool CanRun()
    {
        if (agent.runAway == false)
        {
            return false;
        }

        if (agent.runAway == true)
        {
            return true;
        }

        return false;

    }
}
