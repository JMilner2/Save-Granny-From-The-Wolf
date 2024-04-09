using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction : MonoBehaviour
{
    protected Wolf agent;
    protected FieldOfView FOV;
    protected BaseGoalScript LinkedGoal;

    void Awake()
    {
        agent = GetComponent<Wolf>();
        FOV = GetComponent<FieldOfView>();
    }

    public virtual List<System.Type> GetSupportedGoals()
    { 
        return null; 
    }

     public virtual float Cost()
    {
        return 0f;
    }

    public virtual void OnActivated(BaseGoalScript linkedGoal) 
    {
        LinkedGoal = linkedGoal;
    }

    public virtual void OnDeactivated()
    {
        LinkedGoal = null;
    }

    public virtual void OnTick()
    {

    }

}
