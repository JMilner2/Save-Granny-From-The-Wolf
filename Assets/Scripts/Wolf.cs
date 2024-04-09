using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Wolf : MonoBehaviour
{

    [SerializeField]
    private GameObject[] waypoints;
    [SerializeField]
    private GameObject player; 

    UnityEngine.AI.NavMeshAgent agent;
    private int currPoint;
    public Vector3 lastLocation;
    private Vector3 runAwayLocation;
    public bool chasing = false;
    public bool patrolling = false;
    public bool idle = false;
    public bool runAway = false;
    public bool gameEnd = false;
    public bool IsMoving => agent.velocity.magnitude > float.Epsilon;

    Animator animator;

    
    private void Awake()
    {
        gameEnd = false;
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoBraking = false;
        currPoint = Random.Range(0,waypoints.Length); //gets random start point for wolf from a list of waypoints
        IdleMode();
        transform.position = waypoints[currPoint].transform.position; //moves wolf to start pos
        
    }

    public Vector3 GetRandomRunAwayPoint()  //gets run away location when wolf is hit by a rock
    {
        Vector3 playerPosition = player.transform.position;

        Vector3 awayDirection = transform.position - playerPosition;
        awayDirection.y = 0f; // Keep the direction in the xz-plane

        Vector3 destination = transform.position + awayDirection.normalized * 30.0f;

        UnityEngine.AI.NavMeshHit navHit;

        // Sample a valid NavMesh position near the calculated destination
        UnityEngine.AI.NavMesh.SamplePosition(destination, out navHit, 10.0f, UnityEngine.AI.NavMesh.AllAreas);

        runAwayLocation = navHit.position;
        return runAwayLocation;
    }

    public void SetDestination(Vector3 location)
    {
       agent.destination = location;
    }

    public void IteratePoints() //iterates through the wolfs patrolling points
    {
        if (currPoint < waypoints.Length - 1)
        {
            currPoint++;
        }
        else
        {
            currPoint = 0;
        }
        agent.destination = waypoints[currPoint].transform.position;
    }

    public bool AtDestination()
    {
        if (Vector3.Distance(transform.position,
            waypoints[currPoint].transform.position) <= 2.0f)
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock")) //sets runaway true if it by rock to be used by GOAP
        {
            
            Rigidbody rockRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (rockRigidbody != null && rockRigidbody.velocity.magnitude > 4f)
            {
                runAway = true;
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            gameEnd = true; //Wolf has caught the player so end the game
        }
    }


    public void RanAway()
    {
     if(agent.remainingDistance < 0.5f)
        {
            runAway = false;
        }
    }

    public bool CanSmellPlayer() //immatates smell
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 10f)
        {
            return true;
        }
        return false;

    }

    public void RunAwayMode() //sets relavant speed for animator and nav agent
    {
        idle = false;
        patrolling = false;
        chasing = false;
        runAway = true;
        animator.SetFloat("Speed", 6f);
        agent.speed = 6f;
    }

    public void ChaseMode() //sets relavant speed for animator and nav agent
    {
        idle = false;
        patrolling = false;
        chasing = true;
        runAway = false;
        animator.SetFloat("Speed", 8f);
        agent.speed = 8f;
    }

    public void ChasePlayer() //chasing the player
    {
        lastLocation = player.transform.position;
        agent.destination = lastLocation;
    }

    public void PatrolMode() //sets relavant speed for animator and nav agent
    {
        chasing = false;
        idle = false;
        patrolling = true;
        runAway = false;
        agent.speed = 3.5f;
        animator.SetFloat("Speed", 3.5f);
    }

    public void IdleMode() //sets relavant speed for animator and nav agent
    {
        chasing = false;
        patrolling = false;
        idle = true;
        runAway = false;
        agent.speed = 0.0f;
        animator.SetFloat("Speed", 0.0f);
    }

}
