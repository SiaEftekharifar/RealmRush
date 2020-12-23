using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Enemy : MonoBehaviour {

    PathFinder pathFinder;

    [SerializeField] int healthPoint = 5;

    [SerializeField] float enemyDestructionTime;
    [SerializeField] Transform spawnedObjectsParent;
    [SerializeField] GameObject deathFx;
    void Start() {

        pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();

        StartCoroutine(FollowPath(path));
    }


    void OnParticleCollision(GameObject other) {

        ProcessHit();
    }

    private void ProcessHit() {

        healthPoint--;
        GameObject fx = Instantiate(deathFx, transform.position, Quaternion.identity);
        fx.transform.parent = spawnedObjectsParent;
        if (healthPoint < 1) {
            KillEnemy();
        }
    }

    private void KillEnemy() {
       
        Destroy(gameObject, enemyDestructionTime); 
    }

    IEnumerator FollowPath(List<WayPoint> path) {

        foreach (WayPoint wayPoint in path) {

            transform.position = wayPoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }

}
