using UnityEngine;

public class HitPoint : MonoBehaviour
{
    [SerializeField]
    private EnemyStat enemyStat;

    [SerializeField]
    private float multiplier;

    public void TakeDamage(float damage)
    {
        enemyStat.TakeDamage(damage * multiplier);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            TakeDamage(other.GetComponent<Bullet>().Damage);
        }
    }
}
