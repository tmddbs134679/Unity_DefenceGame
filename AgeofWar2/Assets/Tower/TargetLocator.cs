using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
     Transform target;
    [SerializeField] float AttackRange = 30f;
    [SerializeField] ParticleSystem targetPS;


    void Update()
    {

        FindClosestTarget();

        AimWeapon();

    }

    void FindClosestTarget()
    {
        Enemy[] enrmies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;

        //max값을 가장 큰 값으로 미리 설정
        float maxDistance = Mathf.Infinity;
        

       foreach (Enemy enemy in enrmies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;

    }


    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
       

        if (targetDistance < AttackRange)
        {
            Attack(true);
            weapon.LookAt(target);
        }
        else
        {
            Attack(false);
        }


    }

    void Attack(bool isActive)
    {
        var emissionModule = targetPS.emission;

        emissionModule.enabled = isActive;

        
    }
}
