using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : BaseAction
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(IdleGoal)});

    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public override void OnActivated(BaseGoalScript linkedAction)
    {
        base.OnActivated(linkedAction);
        agent.IdleMode();
    }

    public override float Cost()
    {
        return 0f;
    }
}
