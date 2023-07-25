using UnityEngine;

public class EnemysBullet : Bullet
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealthController>().TakeDamage(Damage);
        }
        else if (other.tag == "PlayersBody")
        {
            other.GetComponentInParent<PlayerHealthController>().TakeDamage(Damage);
        }
    }
}
