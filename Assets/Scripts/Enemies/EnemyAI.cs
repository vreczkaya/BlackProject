using System.Collections;
using UnityEngine;
using System.Linq;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private NPCMoveController moveController;

    [SerializeField]
    private Collider[] targetsInViewRadius;

    [SerializeField]
    private int calmVisible;
    [SerializeField]
    private int argVisible;
    [SerializeField]
    private int angleView;

    [SerializeField]
    private LayerMask findEnemyMask;
    [SerializeField]
    private LayerMask trackEnemyMask;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform targetMemory;

    private Transform head;
    private Animator animator;
    private EnemyStat enemyStat;

    [SerializeField]
    private Weapon activeWeapon;
    [SerializeField]
    private Transform mainTransform;

    private Vector3 lastTargetPosition;

    private void Start()
    {
        StartCoroutine("FindTarget", 0.3f);

        enemyStat = GetComponentInParent<EnemyStat>();
        animator = enemyStat.GetAnimator();

        head = animator.GetBoneTransform(HumanBodyBones.Head).transform;
    }

    private void Update()
    {
        if (!enemyStat.IsDead)
        {
            if (target == null && targetMemory == null)
            {
                moveController.Patrol();
            }
            else if (target != null && targetMemory != null)
            {
                Shoot();
            }
            else if (target == null && targetMemory != null)
            {
                animator.SetBool("isAiming", true);
                Pursuit();
            }
        }
    }

    #region Поиск врага

    IEnumerator FindTarget(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindTargetRayCast();
        }
    }

    private void SortTargets()
    {
        targetsInViewRadius = Physics.OverlapSphere(transform.position, calmVisible, findEnemyMask);

        Collider temp;

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            for (int j = i + 1; j < targetsInViewRadius.Length; j++)
            {
                float dist1 = Vector3.Distance(transform.position, targetsInViewRadius[i].transform.position);
                float dist2 = Vector3.Distance(transform.position, targetsInViewRadius[j].transform.position);

                if (dist1 > dist2)
                {
                    temp = targetsInViewRadius[i];
                    targetsInViewRadius[i] = targetsInViewRadius[j];
                    targetsInViewRadius[j] = temp;
                }
            }
        }
    }


    private void FindTargetRayCast()
    {
        SortTargets();

        if (target == null)
        {
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                if (moveController.CalculateAngleToPoint(targetsInViewRadius[i].transform.position) <= angleView &&
                    moveController.CalculateAngleToPoint(targetsInViewRadius[i].transform.position) >= -angleView)
                {
                    Ray ray = new Ray(head.position, targetsInViewRadius[i].transform.position - head.position);

                    RaycastHit[] hit = Physics.RaycastAll(ray, 15, trackEnemyMask).OrderBy(h => h.distance).ToArray();

                    for (int j = 0; j < hit.Length; j++)
                    {
                        Debug.DrawRay(head.position, 
                            (targetsInViewRadius[i].transform.position) - head.position, Color.red, 2);
                        if (hit[j].transform == transform)
                        {
                            continue;
                        }
                        else if (hit[j].transform.tag != "Player")
                        {
                            break;
                        }
                        else if (hit[j].transform.tag == "Player")
                        {
                            target = targetsInViewRadius[i].transform;
                            targetMemory = target;
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            if (Vector3.Distance(target.position, head.position) <= argVisible)
            {
                if (moveController.CalculateAngleToPoint(target.position) <= angleView &&
                    moveController.CalculateAngleToPoint(target.position) >= -angleView)
                {
                    Debug.DrawRay(head.position, (target.position) - head.position, Color.red, 2f);

                    Ray ray = new Ray(head.position, (target.position) - head.position);
                    RaycastHit[] hit = Physics.RaycastAll(ray, 15, trackEnemyMask).OrderBy(h => h.distance).ToArray();

                    for (int j = 0; j < hit.Length; j++)
                    {
                        if (hit[j].transform == transform)
                        {
                            continue;
                        }
                        else if (hit[j].transform == target)
                        {
                            break;
                        }
                        else
                        {
                            lastTargetPosition = target.position;
                            target = null;
                            break;
                        }
                    }
                }
                else
                {
                    lastTargetPosition = target.position;
                    target = null;
                }
            }
            else
            {
                lastTargetPosition = target.position;
                target = null;
            }
        }
    }

    #endregion

    #region Стрельба

    public void Shoot()
    {
        if (moveController.Speed > 0)
        {
            moveController.Stop();
        }
        else
        {
            activeWeapon.targetLook = target;
            activeWeapon.isShooting = true;

            if (moveController.IsRotatedToTarget(target.position))
            {
                animator.SetBool("isAiming", true);
            }
        }
    }

    public void DontShoot()
    {
        animator.SetBool("isAiming", false);

        activeWeapon.isShooting = false;
    }

    #endregion

    #region Преследование

    private void Pursuit()
    {
        if (Vector3.Distance(mainTransform.position, lastTargetPosition) < 15f)
        {
            moveController.MoveToPoint(lastTargetPosition);
        }
        else
        {
            targetMemory = null;
        }
    }

    #endregion
}
