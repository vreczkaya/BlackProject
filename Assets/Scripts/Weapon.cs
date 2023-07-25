using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponProperty weaponProperty;
    
    [SerializeField]
    private Transform shootPoint;
    public Transform targetLook;

    [SerializeField]
    private Bullet bullet;

    public Transform leftHandTarget;
    public Transform leftHandAimTarget;

    public bool isShooting;
    [SerializeField]
    private bool isFirstShoot;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float delayShoot;

    [SerializeField]
    private float shootsPerTimeUnit;
    [SerializeField]
    private float timeUnitInSecs;

    private void Start()
    {
        delayShoot = timeUnitInSecs / shootsPerTimeUnit;
    }

    private void Update()
    {
        shootPoint.LookAt(targetLook);
        if (isShooting)
        {
            if (isFirstShoot)
            {
                isFirstShoot = false;
                Shoot();
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= delayShoot)
                {
                    Shoot();
                    timer -= delayShoot;
                }
            }
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }
}
