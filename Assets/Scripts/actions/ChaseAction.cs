using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : BaseAction
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(ChaseGoal) });

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
        agent.ChaseMode();
        agent.ChasePlayer();
    }

    public override void OnDeactivated()
    {

    }

    public override void OnTick()
    {
        agent.ChasePlayer();
    }
}
