using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform[] waypoints; // List of waypoints to follow
    public float followRadius = 10f; // Radius to detect the player
    public LayerMask playerLayer; // Layer that the player is on
    private SphereCollider _detection;
    private NavMeshAgent agent;
    private Transform player;
    private bool isPlayerHidden;

    private void Awake()
    {
        _detection = GetComponent<SphereCollider>(); // Correct the typo in GetComponent
        _detection.isTrigger = true; // Set the SphereCollider as a trigger
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (waypoints.Length > 0)
            SetDestination(waypoints[0].position);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= followRadius)
        {
            // Player is within the follow radius, so follow the player if not hidden.
            if (!isPlayerHidden)
                SetDestination(player.position);
        }
        else
        {
            // Player is outside the follow radius, so follow waypoints.
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                // Choose the next waypoint in the list.
                int nextWaypoint = (Random.Range(0, waypoints.Length) + 1) % waypoints.Length;
                SetDestination(waypoints[nextWaypoint].position);
            }
        }
    }

    private void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            HandleGameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the player is hidden.
            isPlayerHidden = IsPlayerHidden();
            if (isPlayerHidden)
            {
                // Player is hidden, set Destination to the next waypoint.
                int nextWaypoint = (Random.Range(0, waypoints.Length) + 1) % waypoints.Length;
                SetDestination(waypoints[nextWaypoint].position);
            }
            else
            {
                // Player is not hidden, follow the player.
                SetDestination(player.position);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Player is hidden => set Destination to waypoint, if not hidden, it will be handled in Update.
        if (isPlayerHidden)
        {
            int nextWaypoint = (Random.Range(0, waypoints.Length) + 1) % waypoints.Length;
            SetDestination(waypoints[nextWaypoint].position);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Player is no longer hidden.
        isPlayerHidden = false;
    }

    private bool IsPlayerHidden()
    {
        // Raycast to check if the player is hidden. You can adjust the raycast logic as needed.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, followRadius, playerLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                // Player is not hidden.
                return false;
            }
        }
        // Player is hidden.
        return true;
    }

    private void HandleGameOver()
    {
        // Implement your game over logic here.
    }
}
