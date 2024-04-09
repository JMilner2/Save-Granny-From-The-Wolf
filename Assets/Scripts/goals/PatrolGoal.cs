using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGoal : BaseGoalScript
{
    [SerializeField] int wanderPriority = 30;
    [SerializeField] float priorityBuildRate = 1f;
    [SerializeField] float priorityDecayRate = 0.1f;
    float currentPriority = 0f;

    public override void OnTickGoal() //While patrolling decreases patrol priority, when not it increases patrol priority
    {
        if (agent.patrolling)
            currentPriority -= priorityDecayRate * Time.deltaTime;
        else
            currentPriority += priorityBuildRate * Time.deltaTime;
    }

    public override void OnGoalActivated(BaseAction linkedAction)
    {
        base.OnGoalActivated(linkedAction);
        currentPriority = wanderPriority;
    }
    public override int OnCalculatePriority()
    {
        return Mathf.FloorToInt(currentPriority);
    }

    public override bool CanRun()
    {
        return true;
    }
}
