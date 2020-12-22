using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    PathFinder pathFinder;


    void Start() {

        pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();

        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<WayPoint> path) {

        foreach (WayPoint wayPoint in path) {

            transform.position = wayPoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }

}
