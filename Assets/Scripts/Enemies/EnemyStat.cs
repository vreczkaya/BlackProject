using UnityEngine;
using UnityEngine.Events;

public class EnemyStat : Health
{
    [SerializeField]
    private Animator animator;

    private EnemyAI enemyAI;

    private NPCMoveController moveController;

    [SerializeField]
    private Rigidbody[] rigidbodies;

    public bool IsDead = false;

    [HideInInspector]
    public UnityEvent OnDie;

    private int dieAmount = 0;

    protected override void Start()
    {
        base.Start();
        moveController = GetComponentInParent<NPCMoveController>();
        enemyAI = GetComponent<EnemyAI>();
    }

    public Animator GetAnimator() => animator;

    protected override void Die()
    {
        IsDead = true;
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;

            rigidbody.freezeRotation = false;
        }

        moveController.Die();
        enemyAI.DontShoot();

        moveController.enabled = false;
        animator.enabled = false;
        if (dieAmount == 0)
        {
            OnDie?.Invoke();
            dieAmount++;
        }
    }
}
