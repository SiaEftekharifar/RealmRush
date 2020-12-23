using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    [SerializeField] float range;

    //State of each tower
    Transform targetEnemy;

    
    // Update is called once per frame
    void Update() {

        SetTargetEnemy();

        if (targetEnemy) {
            objectToPan.transform.LookAt(targetEnemy);
            CheckDistanceToEnemy();
        }
        else {
            Fire(false);
        }

    }

    private void SetTargetEnemy() {

        var enemiesInScene = FindObjectsOfType<Enemy>();
        if (enemiesInScene.Length == 0) { return;}

        Transform closestEnemy = enemiesInScene[0].transform;

        foreach (var enemy in enemiesInScene) {
            closestEnemy = GetClosestEnemy(closestEnemy, enemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosestEnemy(Transform transformA, Transform transformB) {

        float distToA = Vector3.Distance(transformA.position, gameObject.transform.position);
        float distToB = Vector3.Distance(transformB.position, gameObject.transform.position);

        if (distToA < distToB) {
            return transformA;
        }
            return transformB;
    }

    private void CheckDistanceToEnemy() {

        float distance = Vector3.Distance(targetEnemy.position, transform.position);
        if (distance < range) {
            Fire(true);
        }
        else {
            Fire(false);
        }
    }

    private void Fire(bool isActive) {
        var bullet = GetComponentInChildren<ParticleSystem>().emission;
        bullet.enabled = isActive;
    }

  
}
