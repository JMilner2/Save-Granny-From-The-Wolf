using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : BaseAction
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(PatrolGoal) });

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
        agent.PatrolMode();
        agent.IteratePoints();
    }

    public override void OnDeactivated()
    {

    }

    public override void OnTick()
    {
        if (agent.AtDestination()) //At destination so make new waypoint the destination
        {
            agent.IteratePoints();  
        }

    }
}
