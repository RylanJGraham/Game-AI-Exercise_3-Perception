using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private bool isChasing = false;
    private Camera zombieCamera;

    public float wanderRadius = 10f;
    public float minWanderInterval = 3f;
    public float maxWanderInterval = 6f;
    public GameObject playerObject; // Assign the player GameObject in the Inspector.
    public GameObject cameraObject; // Assign the camera GameObject in the Inspector.

    private float wanderTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = playerObject.transform; // Use the assigned player GameObject.

        // Assign the specified camera GameObject.
        if (cameraObject != null)
        {
            zombieCamera = cameraObject.GetComponent<Camera>();
        }
        else
        {
            Debug.LogError("Camera GameObject not assigned for zombie detection.");
        }

        // Initialize the wander timer with a random value.
        wanderTimer = Random.Range(minWanderInterval, maxWanderInterval);
    }

    void Update()
    {
        if (!isChasing)
        {
            wanderTimer -= Time.deltaTime;

            if (wanderTimer <= 0f)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                wanderTimer = Random.Range(minWanderInterval, maxWanderInterval);
            }
        }
        else
        {
            agent.SetDestination(player.position);
        }

        if (CanSeePlayer())
        {
            isChasing = true;
        }
    }

    public bool CanSeePlayer()
    {
        if (zombieCamera == null || player == null)
        {
            return false;
        }

        // Calculate the player's position in screen space.
        Vector3 screenPoint = zombieCamera.WorldToViewportPoint(player.position);

        // Check if the player is within the camera's frustum.
        if (screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1 && screenPoint.z > 0)
        {
            return true;
        }

        return false;
    }

    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask);

        return navHit.position;
    }

    public void StartChasing(Transform target)
    {
        isChasing = true;
        player = target;
    }
}
