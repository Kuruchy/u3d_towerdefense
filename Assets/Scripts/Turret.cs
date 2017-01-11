using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    private Transform target;
    private Enemy targetEnemy;

    // Create a header to see in Inspector
    [Header("General")]
    public float range = 15f;
    public Transform firePoint;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public float slowPercnt = .5f;

    [Header("Unity Setup Fields")]
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public int damageOverTiime = 30;

    private string enemyTag = "Enemy";

    // Use this for initialization
    void Start () {
        /* This is the way to repeatly call a function when you want
        to define the amount of repetitions per second, and dont use
        the Update method. */

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();

        }else
        {
            // Firing
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
	}

    void LockOnTarget()
    {
        // Update the rotation and use Lerp to make it smooth
        Vector3 direcction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direcction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTiime * Time.deltaTime);
        targetEnemy.Slow(slowPercnt);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        impactEffect.transform.position = target.position + dir.normalized;

    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        
        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    // When selected draw a wired sphere
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
