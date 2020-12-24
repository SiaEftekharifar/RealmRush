using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Enemy : MonoBehaviour {

    PathFinder pathFinder;
    PlayerBaseHealth playerBaseHealth;

    [SerializeField] int healthPoint = 5;

    [SerializeField] float enemyDestructionTime;

    [SerializeField] GameObject deathFxPrefab;
    [SerializeField] GameObject deathGoalFxPrefab;

    float fxDestrcutionDelay = 1f;
    bool isReachedGoal = false;


    void Start() {
        playerBaseHealth = FindObjectOfType<PlayerBaseHealth>();
        pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();

        StartCoroutine(FollowPath(path));
    }


    void OnParticleCollision(GameObject other) {

        ProcessHit();
    }

    private void ProcessHit() {

        healthPoint--;
        
        if (healthPoint < 1) {
            KillEnemy();
        }
    }

    private void KillEnemy() {
        if (isReachedGoal) {
            IntantiateFXs(deathGoalFxPrefab);
        }
        else {
            IntantiateFXs(deathFxPrefab);
        }
        Destroy(gameObject, enemyDestructionTime);
    }

    private void IntantiateFXs(GameObject FX) {

        GameObject deathFx = Instantiate(FX, transform.position, Quaternion.identity);
        deathFx.transform.parent = gameObject.transform;
        Destroy(deathFx, fxDestrcutionDelay);
    }

    IEnumerator FollowPath(List<WayPoint> path) {

        foreach (WayPoint wayPoint in path) {

            transform.position = wayPoint.transform.position;
            yield return new WaitForSeconds(0.5f);
        }

        isReachedGoal = true;
        KillEnemy();
        playerBaseHealth.FriendlyBaseDamage();
    }

}
