using UnityEngine;
using UnityEngine.AI;

public class NPCMoveController : MonoBehaviour
{
    public Transform[] MovePoints;

    private NavMeshAgent agent;
    private Animator animator;

    public bool CircularRoute;
    public bool IsFast;
    public int CurrentWayPoint;

    public Vector3 PointToMove;

    public float Speed, WalkSpeed, RunSpeed, RotationSpeed;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.updateRotation = false;
    }

    public void Patrol()
    {
        if (MovePoints.Length >= 1)
        {
            if (MovePoints.Length > CurrentWayPoint)
            {
                if (Vector3.Distance(transform.position, MovePoints[CurrentWayPoint].position) < 0.2f)
                {
                    CurrentWayPoint++;
                }
                else
                {
                    MoveToPoint(MovePoints[CurrentWayPoint].position);
                }
            }
            else if (MovePoints.Length == CurrentWayPoint)
            {
                if (CircularRoute)
                {
                    CurrentWayPoint = 0;
                }
                else
                {
                    if (Vector3.Distance(transform.position, MovePoints[CurrentWayPoint - 1].position) < 0.2f)
                    {
                        Stop();
                    }
                    else
                    {
                        MoveToPoint(MovePoints[CurrentWayPoint - 1].position);
                    }
                }
            }
        }
        else
        {
            Stop();
        }
    }

    public void MoveToPoint(Vector3 position)
    {
        if (PointToMove != position)
        {
            agent.SetDestination(position);
            PointToMove = position;
        }

        if (agent.hasPath)
        {
            if (IsFast)
            {
                Speed = Mathf.MoveTowards(Speed, RunSpeed, Time.deltaTime * 3);
            }
            else
            {
                Speed = Mathf.MoveTowards(Speed, WalkSpeed, Time.deltaTime * 3);
            }

            animator.SetFloat("speed", Speed);
            agent.speed = Speed;

            IsRotatedToTarget(agent.path.corners[1]);
        }
        else
        {
            IsRotatedToTarget(PointToMove);
        }
    }

    public float CalculateAngleToPoint(Vector3 position)
    {
        Vector3 targetPosition = position;
        targetPosition.y = transform.position.y;
        Vector3 targetDirection = targetPosition - transform.position;
        Vector3 forward = transform.forward * 0.001f;
        float angleBetween = Vector3.SignedAngle(targetDirection, forward, Vector3.up);

        return angleBetween * -1;
    }

    public bool IsRotatedToTarget(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);

        return transform.rotation == lookRotation;
    }

    public void Die()
    {
        agent.enabled = false;
    }  

    public void Stop()
    {
        if (Speed > 0)
        {
            agent.SetDestination(transform.position);
            Speed = Mathf.MoveTowards(Speed, 0, Time.deltaTime * 15);
            animator.SetFloat("speed", Speed);
            agent.speed = Speed;
        }
    }
}
