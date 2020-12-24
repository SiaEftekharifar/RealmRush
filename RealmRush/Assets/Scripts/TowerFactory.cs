using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField]
    int maxTowers = 4;

    [SerializeField] Tower towerPrefab;


    Queue<Tower> queue = new Queue<Tower>();

    public void AddTower(WayPoint baseWaypoint) {


        int towerNumer = queue.Count;
        print(queue.Count);
        if (towerNumer < maxTowers) {
            InstantiateTowers(baseWaypoint);
        }
        else {
            MoveExistingTower(baseWaypoint);
        }

    }



    private void InstantiateTowers(WayPoint baseWaypoint) {

        Tower newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        baseWaypoint.isPlaceable = false;
        newTower.transform.parent = gameObject.transform;
        newTower.baseWaypoint = baseWaypoint;

        queue.Enqueue(newTower);

    }

    private void MoveExistingTower(WayPoint baseWaypoint) {

        Tower oldTower = queue.Dequeue();
        oldTower.baseWaypoint.isPlaceable = true;
        oldTower.transform.position = baseWaypoint.transform.position;
        baseWaypoint.isPlaceable = false;
        queue.Enqueue(oldTower);
    }

}