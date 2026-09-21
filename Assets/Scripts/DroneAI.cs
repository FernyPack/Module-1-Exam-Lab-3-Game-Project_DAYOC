using UnityEngine;
using UnityEngine.AI;

public class DroneAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform[] waypoints;
    public Battery battery;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;

    [Header("Detection")]
    public float viewDistance = 8f;
    public float sprintBonus = 5f;
    public float flashlightBonus = 3f;
    public float lightBonus = 4f;

    [Header("Audio")]
    public AudioSource moveAudio;
    public AudioSource detectAudio;

    private NavMeshAgent agent;
    private int currentWaypoint;
    private bool chasing;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (battery == null)
            battery = player.GetComponent<Battery>();

        agent.speed = patrolSpeed;

        GoToNextWaypoint();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            if (!chasing)
            {
                chasing = true;

                if (detectAudio != null)
                    detectAudio.Play();
            }

            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);
        }
        else
        {
            if (chasing)
            {
                chasing = false;
                agent.speed = patrolSpeed;
                GoToNextWaypoint();
            }

            if (!agent.pathPending && agent.remainingDistance <= 0.5f)
            {
                GoToNextWaypoint();
            }
        }

        if (moveAudio != null)
        {
            if (agent.velocity.magnitude > 0.1f)
            {
                if (!moveAudio.isPlaying)
                    moveAudio.Play();

                moveAudio.pitch = chasing ? 1.2f : 1f;
            }
            else
            {
                moveAudio.Stop();
            }
        }
    }

    bool CanSeePlayer()
    {
        float detectionDistance = viewDistance;

        bool moving =
            Input.GetAxisRaw("Horizontal") != 0 ||
            Input.GetAxisRaw("Vertical") != 0;

        bool sprinting =
            Input.GetKey(KeyCode.LeftShift) && moving;

        if (sprinting)
            detectionDistance += sprintBonus;

        if (battery != null &&
            battery.flashlight != null &&
            battery.flashlight.enabled)
        {
            detectionDistance += flashlightBonus;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        return distance <= detectionDistance;
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        agent.SetDestination(waypoints[currentWaypoint].position);

        currentWaypoint++;

        if (currentWaypoint >= waypoints.Length)
            currentWaypoint = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnDrawGizmosSelected()
    {
        float radius = viewDistance;

        Gizmos.color = chasing ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}