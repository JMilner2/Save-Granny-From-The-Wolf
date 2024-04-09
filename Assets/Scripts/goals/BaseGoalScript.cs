using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public interface IGoal
{

    int OnCalculatePriority();
    void OnTickGoal();
    void OnGoalActivated(BaseAction linkedAction);
    void OnGoalDeactivated();
    bool CanRun();
   
}


public class BaseGoalScript : MonoBehaviour, IGoal
{
    protected Wolf agent;
    protected FieldOfView FOV;
    protected BaseAction LinkedAction;

    
    void Awake()
    {
        agent = GetComponent<Wolf>();
        FOV = GetComponent<FieldOfView>();
    }

    void Start()
    {

    }

    void Update()
    {
        OnTickGoal();
      
    }

    public virtual int OnCalculatePriority()
    {
        return -1;
    }

    public virtual void OnTickGoal()
    {

    }

    public virtual void OnGoalActivated(BaseAction linkedAction)
    {
        LinkedAction = linkedAction;
    }

    public virtual void OnGoalDeactivated()
    {
        LinkedAction = null;
    }

    public virtual bool CanRun()
    {
        return false;
    }

}
