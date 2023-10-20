using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform[] waypoints; // List of waypoints to follow
    public float followRadius = 10f; // Radius to detect the player

    private NavMeshAgent agent;
    private Transform player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assumes the player has a "Player" tag.

        if (waypoints.Length > 0)
            SetDestination(waypoints[0].position);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= followRadius)
        {
            // Player is within the follow radius, so follow the player.
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
            // You can add game over logic here (e.g., reload level or show a game over screen).
        }
    }
}
