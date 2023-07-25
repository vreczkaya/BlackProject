using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int speed;

    [SerializeField]
    private GameObject explosion;

    private Vector3 lastPosition;

    public float Damage;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        RaycastHit hit;

        if (Physics.Linecast(lastPosition, transform.position, out hit))
        {
            if (Damage == 20)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }

        lastPosition = transform.position;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("!@#$%^&()");
            other.GetComponentInParent<EnemyStat>().TakeDamage(Damage);
        }
    }
}
