using System;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    
    private float rotationSpeed;   
    private float range;
    public GameObject bulletPrefab;
    private Transform currentTarget;
    public GameObject barrel;
    private float fireRate;
    private float fireTimer = 0f;
    private string myTag;
    private string enemyTag;
    
    private int damage;
    private float bulletSpeed;
    Vector2 transform2dCenter; //should fix turret move and firing ranges not matching exactly
    [Range(0f, 1f)]
    public float idleRotationFactor = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Unit parent = GetComponentInParent<Unit>();
        range = parent.attackRange;
        myTag = parent.getMyTag();
        enemyTag = parent.getEnemyTag();
        fireRate = parent.fireRate;
        rotationSpeed = parent.turretRotationSpeed;
        damage = parent.damage;
        bulletSpeed = parent.bulletSpeed;
        transform2dCenter = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform2dCenter = transform.position;
        if (currentTarget == null || !IsTargetInRange(currentTarget))
        {
            //idle rotation
            transform.Rotate(rotationSpeed* idleRotationFactor * Time.deltaTime*Vector3.forward); 
                

            currentTarget = FindTarget();
        }

        if (currentTarget != null &&  IsTargetInRange(currentTarget))
        {
            RotateTurret();
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0)
            {
                Shoot();
                fireTimer = fireRate;
            }
            
        }
    }

    private void Shoot()
    {
    //    Debug.Log("shooting");
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = barrel.transform.position;
        bullet.transform.localRotation = barrel.transform.rotation;
        bullet.GetComponent<Bullet>().Initialize(range, myTag, enemyTag, damage, bulletSpeed);
        
    }


    Transform FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform2dCenter, range); //TEST
        Transform bestTarget = null;
        float smallestAngle = Mathf.Infinity;
    
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(enemyTag))
            {
                Vector2 direction = hit.transform.position - transform.position;  //TEST
                float angle = Vector2.Angle(transform.right, direction);
                if (angle < smallestAngle)
                {
                    smallestAngle = angle;
                    bestTarget = hit.transform;
                }
            }
        }
        return bestTarget;
    }
    
    bool IsTargetInRange(Transform target)
    {
        if (target == null) return false;
        return Vector2.Distance(transform2dCenter, target.position) <= range; //TEST
    }

    void RotateTurret()
    {
        Vector2 direction = (currentTarget.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
    
}
