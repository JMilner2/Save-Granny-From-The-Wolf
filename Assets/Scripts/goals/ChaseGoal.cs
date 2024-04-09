using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class ChaseGoal : BaseGoalScript
{
    [SerializeField] int chasePriority = 60;
    int currentPriority = 0;

    public override void OnTickGoal()
    {
        currentPriority = 0;

        if (FOV.CanSeePlayer() == false) //if player cant be seen
        {
            if (agent.CanSmellPlayer() == true)  //checks if player can be smelt
            {
                currentPriority = chasePriority;
            }
            else
            {
                currentPriority = 0;
            }
        }

        if (FOV.CanSeePlayer() == true)
        {
            currentPriority = chasePriority;
        }
    }

    public override void OnGoalDeactivated()
    {
       base.OnGoalDeactivated();
    }


    public override int OnCalculatePriority()
    {
        return Mathf.FloorToInt(currentPriority); //returns calculated priority to planner
    }

    public override bool CanRun()
    {
        if(FOV.CanSeePlayer() == false && !agent.CanSmellPlayer())   //can only run if the player can be seen or smelt
        {
          return false;
        }

        if (FOV.CanSeePlayer() == true || agent.CanSmellPlayer())
        {
            return true;
        }

        return false;

    }

}
