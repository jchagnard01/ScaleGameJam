using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HostilAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform playerTransform;

    [Header("Layers")]
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private LayerMask playerLayerMask;

    [Header("Patrol Settings")]
    [SerializeField] private float patrolRadius = 10f;
    private Vector3 currentPatrolPoint;
    private bool hasPatrolPoint;

    [Header("Combat Settings")]
    [SerializeField] private float attackCooldown = 1f;
    private bool isOnAttackCooldown;

    [Header("Detection Ranges")]
    [SerializeField] private float visionRange = 20f;
    [SerializeField] private float engagementRange = 10f;

    private bool isPlayerVisible;
    private bool isPlayerInRange;


    private void Awake()
    {
        if(playerTransform == null)
        {
            GameObject playerObject = GameObject.Find("Player");
            if (playerObject != null) playerTransform = playerObject.transform;

            if (navAgent == null) navAgent = GetComponent<NavMeshAgent>(); 
        }
    }

    private void Update()
    {
        DetectPlayer();
        UpdateBehaviorState();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, engagementRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    /// <summary>
    /// Checks a sphere around the enemy to see if both the player is Visible and in Engagement Range
    /// </summary>
    private void DetectPlayer()
    {
        isPlayerVisible = Physics.CheckSphere(transform.position, visionRange, playerLayerMask);
        isPlayerInRange = Physics.CheckSphere(transform.position, engagementRange, playerLayerMask);
    }

    /// <summary>
    /// Finds a point on the nav mesh for the enemy to patrol to
    /// </summary>
    private void FindPatrolPoint()
    {
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);

        Vector3 potentialPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(potentialPoint, -transform.up, 2f, terrainLayer))
        {
            currentPatrolPoint = potentialPoint;
            hasPatrolPoint = true;
        }
    }

    /// <summary>
    /// Sets a point on the nav mesh for the AI to move to
    /// </summary>
    private void PerformPatrol()
    {
        if (!hasPatrolPoint) FindPatrolPoint();
        if (hasPatrolPoint) navAgent.SetDestination(currentPatrolPoint);
        if (Vector3.Distance(transform.position, currentPatrolPoint) < 1f) hasPatrolPoint = false;
    }

    /// <summary>
    /// Marks the player as the destination so that the enemy 
    /// will chase the player.
    /// </summary>
    private void PerformChase()
    {
        if(playerTransform != null)
        {
            navAgent.SetDestination(playerTransform.position);
        }
    }

    /// <summary>
    /// Performs the attack function, then starts the 
    /// attack cooldown.
    /// </summary>
    private void PerformAttack()
    {
        navAgent.SetDestination(transform.position);

        if (playerTransform != null) transform.LookAt(playerTransform);
        if(!isOnAttackCooldown)
        {
            Debug.Log("Hostile AI attack!");
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    /// <summary>
    /// Coroutine for delaying the enemy from attacking 
    /// until the <see cref="attackCooldown"/> has passed.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCooldownRoutine()
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }

    /// <summary>
    /// Primary State Machine function. Determines what the enemy will do at a given point.
    /// Depends on the <see cref="isPlayerInRange"/> and <see cref="isPlayerVisible"/>
    /// </summary>
    private void UpdateBehaviorState()
    {
        if (!isPlayerInRange && !isPlayerVisible) PerformPatrol();
        else if (isPlayerVisible && !isPlayerInRange) PerformChase();
        else if (isPlayerVisible && isPlayerInRange) PerformAttack();
    }
}
