using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SpeedyEnemyAI : MonoBehaviour
{
    [SerializeField] private EndGoal endGoal;

    [SerializeField] private List<Transform> waypoints;

    [SerializeField] private float DistanceToWaypoint;

    [SerializeField] private float WaitTimeOnWaypoint;

    [SerializeField]
    public GridManager grid;

    Transform currentWaypointTarget;
    private int index = 0;

    private NavMeshAgent agent;
    private Animator anim;

    // Check if the enemy is moving
    private bool moving = true;
    // Check if the enemy is at the EndGoal
    private bool atEndGoal = false;

    // StateMachine for the enemy
    enum enemyStates
    {
        moveToEndGoal,  // Standard state for moving to the endGoal
        atEndGoal       // State for when the enemy is at the EndGoal
    };

    // Declare and initialize a variable to hold the currentstate of the enemy
    [SerializeField] enemyStates enemyCurrentState = enemyStates.moveToEndGoal;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //anim = new Animator();

        endGoal = GameObject.FindObjectOfType<EndGoal>();
        grid = GameObject.FindObjectOfType<GridManager>();

        // Add the waypoints to the list of waypoints
        waypoints.Add(GameObject.Find("Waypoint1").transform);
        waypoints.Add(GameObject.Find("Waypoint2").transform);
        waypoints.Add(GameObject.Find("Waypoint3").transform);

        //grid.GetwaypointsPositions(waypoints);

        // Check if no components are missing
        if (agent == null) { UnityEngine.Debug.LogError("NavMeshAgent is missing"); }
        //if (animator == null) { Debug.LogError("Animator is missing"); }


        // Start moving towards the first waypoint
        // If there are waypoints and if the first waypoint is not null
        if (waypoints.Count > 0 && waypoints[0] != null)
        {
            // Set target waypoint
            currentWaypointTarget = waypoints[index];

            // Start moving towards the next target waypoint
            agent.SetDestination(currentWaypointTarget.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current speed of the agent and set the speed on the animator
        //float speedPercent = agent.velocity.magnitude / agent.speed;
        //animator.SetFloat("speed", speedPercent);

        StateHandler();

        // StateMachine for the enemy states
        switch (enemyCurrentState)
        {
            case enemyStates.moveToEndGoal:
                MoveToEndGoal();
                break;
            case enemyStates.atEndGoal:
                EnemyAtEndGoal();
                break;
            default:
                break;
        }
    }

    private void StateHandler()
    {
        // Change the enemyState depending on te situation
        if (!atEndGoal) enemyCurrentState = enemyStates.moveToEndGoal;
        if (atEndGoal) enemyCurrentState = enemyStates.atEndGoal;
    }

    //================================
    //          Waypoints
    //================================

    private void MoveToEndGoal()
    {
        // State Move towards the endGoal

        // Check if the enemy got a Waypoint as target
        if (currentWaypointTarget != null)
        {
            // Walk to the next waypoint
            // Check if the agent is at the target waypoint position
            if ((Vector3.Distance(transform.position, currentWaypointTarget.position) <= DistanceToWaypoint) && moving)
            {
                // Set moving to false to prevent an infinite loop
                moving = false;
                StartCoroutine("MoveToNextWaypoint");
            }
        }
    }

    IEnumerator MoveToNextWaypoint()
    {
        index++;

        if (index < waypoints.Count)
        {
            if (index == 1)
            {
                // Delay before going to the next waypoint
                yield return new WaitForSeconds(WaitTimeOnWaypoint);
            }

            // Set the currentTarget the same as the next waypoint
            currentWaypointTarget = waypoints[index];
        }
        // If the enemy is at the last waypoint atEnd is true
        else if (index == waypoints.Count)
        {
            atEndGoal = true;
        }

        // Move to the position of the target
        agent.SetDestination(currentWaypointTarget.position);
        moving = true;
    }

    private void EnemyAtEndGoal()
    {
        // Kill this enemy
        Destroy(this.gameObject);

        // Take one live away from the endGoal
        endGoal.setEndGoalLivesMinusOne();
    }
}


/*private void MoveToLastWaypoint()
{
    // Method for moving to the last selected waypoint, so that the enemy doesn't get softlocked after attacking a turret

    // If the enemy is at the last waypoint atEnd is true
    if (index == waypoints.Count)
    {
        atEndGoal = true;
    }

    // Move to the position of the target
    agent.SetDestination(currentWaypointTarget.position);
    moving = true;
}*/