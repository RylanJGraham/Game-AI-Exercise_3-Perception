using UnityEngine;
using UnityEngine.AI;

public class PlayerWander : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;
    private Vector3 randomDestination;
    private bool isWalking = false;

    public float wanderRadius = 5f;
    public float minWanderTimer = 2f;
    public float maxWanderTimer = 6f;
    public AnimationClip walkAnimation;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Set the initial random destination
        SetRandomDestination();

        // Start wandering with a random timer
        InvokeRepeating("Wander", 0, Random.Range(minWanderTimer, maxWanderTimer));
    }

    private void SetRandomDestination()
    {
        // Generate a random position within the specified radius
        randomDestination = Random.insideUnitSphere * wanderRadius;
        randomDestination += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDestination, out hit, wanderRadius, 1);
        randomDestination = hit.position;
    }

    private void Wander()
    {
        // Set a new random destination
        SetRandomDestination();

        // Move the agent to the destination
        agent.SetDestination(randomDestination);

        // Set walking animation
        animator.SetBool("IsWalking", true);
        PlayWalkAnimation();

        // Schedule stopping the animation
        Invoke("StopWalking", Random.Range(minWanderTimer - 0.5f, maxWanderTimer - 0.5f));
    }

    private void PlayWalkAnimation()
    {
        if (animator && walkAnimation)
        {
            animator.CrossFadeInFixedTime("Walk", 0.1f); // "Walk" is the name of your walk animation state
        }
    }

    private void StopWalking()
    {
        // Stop walking animation
        animator.SetBool("IsWalking", false);
    }
    
}