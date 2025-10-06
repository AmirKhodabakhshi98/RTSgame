using System;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    
    public float rotationSpeed = 5f;   
    private float range;
    public string enemyTag = "EnemyUnit";
    public GameObject bulletPrefab;
    private Transform currentTarget;
    public GameObject barrel;
    public float fireRate = 5f;
    private float fireTimer = 0f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        range = GetComponentInParent<PlayerUnit>().attackRange;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null || !IsTargetInRange(currentTarget))
        {
            currentTarget = FindTarget();
        }

        if (currentTarget != null)
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
        Debug.Log("shooting");
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = barrel.transform.position;
        bullet.transform.localRotation = barrel.transform.rotation;
        bullet.GetComponent<Bullet>().Initialize();
        
    }


    Transform FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
        Transform bestTarget = null;
        float smallestAngle = Mathf.Infinity;
    
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(enemyTag))
            {
                Vector2 direction = hit.transform.position - transform.position;
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
        return Vector2.Distance(transform.position, target.position) <= range;
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
