using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPPlanner : MonoBehaviour
{
    private BaseGoalScript[] goals;
    private BaseAction[] actions;

    private BaseGoalScript activeGoal;
    private BaseAction activeAction;

    private void Awake()
    {
        goals = GetComponents<BaseGoalScript>();
        actions = GetComponents<BaseAction>();
    }

    // Update is called once per frame
    void Update()
    {
        BaseGoalScript bestGoal = null;
        BaseAction bestAction = null;

        foreach (var goal in goals)
        {
            goal.OnTickGoal();  // Ticks goal

            if (!goal.CanRun())  // Checks if it can run
            {
                continue;
            }

            // Checks if it's a worse priority
            if (!(bestGoal == null || goal.OnCalculatePriority() > bestGoal.OnCalculatePriority()))
            {
                continue;
            }

            // Finds the best cost action
            BaseAction candidateAction = null;
            foreach (var action in actions)
            {
                if (!action.GetSupportedGoals().Contains(goal.GetType()))
                {
                    continue;
                }

                // Found the best action 
                if (candidateAction == null || action.Cost() < candidateAction.Cost())
                {
                    candidateAction = action;
                }
            }

            if (candidateAction != null) // Has an action been found
            {
                bestGoal = goal;
                bestAction = candidateAction;
            }
        }

        if (activeGoal == null) // There is no current goal
        {
            SetActiveGoalAndAction(bestGoal, bestAction);
        }
        else if (activeGoal == bestGoal) // No change in goal
        {
            if (activeAction != bestAction) // Action changed
            {
                activeAction.OnDeactivated();
                activeAction = bestAction;
                activeAction.OnActivated(activeGoal);
            }
        }
        else if (activeGoal != bestGoal) // Either no valid goal OR a new goal
        {
            ResetActiveGoalAndAction(bestGoal, bestAction);
        }

        if (activeAction != null) // Tick action
        {
            activeAction.OnTick();
        }
    }

    private void SetActiveGoalAndAction(BaseGoalScript goal, BaseAction action)
    {
        activeGoal = goal;
        activeAction = action;

        if (activeAction != null)
        {
            activeAction.OnActivated(activeGoal);
        }
        if (activeGoal != null)
        {
            activeGoal.OnGoalActivated(activeAction);
        }
    }

    private void ResetActiveGoalAndAction(BaseGoalScript goal, BaseAction action)
    {
        activeGoal.OnGoalDeactivated();
        activeAction.OnDeactivated();

        SetActiveGoalAndAction(goal, action);
    }
}
