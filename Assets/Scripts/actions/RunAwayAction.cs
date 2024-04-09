using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayAction : BaseAction
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(RunAwayGoal) });

    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public override float Cost()
    {
        return 0f;
    }

    public override void OnActivated(BaseGoalScript linkedAction)
    {
        base.OnActivated(linkedAction);
        agent.RunAwayMode();
        Vector3 location = agent.GetRandomRunAwayPoint();
        agent.SetDestination(location);
    }

    public override void OnDeactivated()
    {

    }

    public override void OnTick()
    {
        agent.RanAway();
    }
}
