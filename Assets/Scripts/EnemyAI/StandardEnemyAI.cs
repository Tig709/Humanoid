using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class StandardEnemyAI : MonoBehaviour
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

    [Header("AttackTurret")]

    private LayerMask turretLayer;

    private GameObject closestTurret;

    private bool randomnesForAttackingTurret = true;

    private bool willAttackTurret;

    private bool alreadyAttacked;

    private bool justAttackedTurret;

    private bool justMovedToTurret;

    [SerializeField] private int minRandom, maxRandom, randomLower;

    [SerializeField]
    private Transform enemyBulletPrefab;

    [SerializeField] private float timeBetweenAttacks;

    // Enemy Range
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool turretInSightRange, turretInAttackRange;

    // Timer for randomness
    [SerializeField] private float detectTimer;
    [SerializeField] private float detectTimerCD;

    // StateMachine for the enemy
    enum enemyStates
    {
        moveToEndGoal,  // Standard state for moving to the endGoal
        moveToTurret,   // State for moving to the turret it detected
        attackTurret,   // State for attacking the turrit it detected
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

        // Assign the layer of the turret to the turretLayerMask
        turretLayer = LayerMask.GetMask("Turret");

        // Assign the timer
        detectTimer = 0f;

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

        detectTimer += Time.deltaTime;

        Detection();

        StateHandler();

        // StateMachine for the enemy states
        switch (enemyCurrentState)
        {
            case enemyStates.moveToEndGoal:
                MoveToEndGoal();
                break;
            case enemyStates.moveToTurret:
                MoveToTurret();
                break;
            case enemyStates.attackTurret:
                AttackTurret();
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
        if (!turretInSightRange && !willAttackTurret && !turretInAttackRange && !atEndGoal) enemyCurrentState = enemyStates.moveToEndGoal;
        if (turretInSightRange && willAttackTurret && !turretInAttackRange && !atEndGoal) enemyCurrentState = enemyStates.moveToTurret;
        if (turretInSightRange && willAttackTurret && turretInAttackRange && !atEndGoal) enemyCurrentState = enemyStates.attackTurret;
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
            // If the enemy just attacked a turret then go to the last selected waypoint
            if (justAttackedTurret || justMovedToTurret)
            {
                MoveToLastWaypoint();
                justAttackedTurret = false;
                justMovedToTurret = false;
            }
            else // Walk to the next waypoint
            {
                // Check if the agent is at the target waypoint position
                if ((Vector3.Distance(transform.position, currentWaypointTarget.position) <= DistanceToWaypoint) && moving)
                {
                    // Set moving to false to prevent an infinite loop
                    moving = false;
                    StartCoroutine("MoveToNextWaypoint");
                }
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

    private void MoveToLastWaypoint()
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
    }

    private void EnemyAtEndGoal()
    {
        // Kill this enemy
        Destroy(this.gameObject);

        // Take one live away from the endGoal
        endGoal.setEndGoalLivesMinusOne();
    }

    //===========================================
    //        Turret detection/attack
    //===========================================


    private void Detection()
    {
        // RNG for detection
        if (detectTimer > detectTimerCD)
        {
            detectTimer = 0f;

            randomnesForAttackingTurret = true;
        }

        // Check for sight and attack range
        turretInSightRange = Physics.CheckSphere(transform.position, sightRange, turretLayer);

        // Check if the enemy wants to attack the turret
        if (turretInSightRange && randomnesForAttackingTurret && !turretInAttackRange)
        {
            randomnesForAttackingTurret = false;

            // Set here how random you want it to be
            if (Random.Range(minRandom, maxRandom) < randomLower)
            {
                willAttackTurret = true;
            }
            else
            {
                willAttackTurret = false;
            }
        }

        // If the enemy just moved to the turret check if a turret is in attack range
        if (justMovedToTurret)
        {
            turretInAttackRange = Physics.CheckSphere(transform.position, attackRange, turretLayer);
        }
    }

    private GameObject GetClosestTurret(GameObject[] turrets)
    {
        // Method for getting the closestTurret to the enemy
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        // Check each potential Turret as a Target
        foreach (GameObject potentialTarget in turrets)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

    private void MoveToTurret()
    {
        // Make list of all turrets
        GameObject[] possibleTurretTargets = GameObject.FindGameObjectsWithTag("Turret");

        justMovedToTurret = true;

        // If there are turrets get the closest turret and set the enemy destination to the closest turret position
        if (possibleTurretTargets.Length > 0)
        {
            closestTurret = GetClosestTurret(possibleTurretTargets);
            agent.SetDestination(closestTurret.transform.position);
        }
        else
        {
            enemyCurrentState = enemyStates.moveToEndGoal;
        }
    }

    private void AttackTurret()
    {
        // Make list of all turrets
        GameObject[] possibleTurretTargets = GameObject.FindGameObjectsWithTag("Turret");
        closestTurret = GetClosestTurret(possibleTurretTargets);

        justAttackedTurret = true;

        // Stop the enemy from moving into the turret
        agent.SetDestination(transform.position);

        // If the turret is dead or doesn't exist anymore return to the moveToEndGoal State
        if (closestTurret == null)
        {
            detectTimer = 0f;
            enemyCurrentState = enemyStates.moveToEndGoal;
        }

        // Look at the closest turret
        transform.LookAt(closestTurret.transform.position);         // ERROR IS KNOW ON THIS LINE

        if (!alreadyAttacked)
        {
            // Attack code here
            var bullet = Instantiate(enemyBulletPrefab);
            bullet.transform.position = transform.position;
            bullet.transform.LookAt(closestTurret.transform.position);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}

// Draw a trail behind the attack of the enemy
/*private void OnDrawGizmosSelected()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, attackRange);
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, sightRange);
}*/


//Physics.OverlapCapsule(transform.position, sightRange, turretLayer);

/*foreach (var collider in collidersInRange)
{
    var colliderObject = collider.gameObject;
    var resultComponent = colliderObject.GetComponent<T>();
    if (resultComponent != null)
        yield return resultComponent;
}*/