using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] WayPoint startWayPoint, endWayPoint;
    List<WayPoint> path = new List<WayPoint>();

    Dictionary<Vector2Int, WayPoint> worldGrid = new Dictionary<Vector2Int, WayPoint>();
    Queue<WayPoint> queue = new Queue<WayPoint>();


    WayPoint searchCenter;

    Vector2Int[] directions = {
                                  Vector2Int.up,
                                  Vector2Int.right,
                                  Vector2Int.down,
                                  Vector2Int.left, 
                              };

    bool isRunning = true;


    public List<WayPoint> GetPath() {

        if (path.Count == 0) {
            CalculatePath();
        }
        return path;
    }

    private void CalculatePath() {
        LoadBlockToDictionary();
        BreadthFirstSearch();
        FindPath();
    }

    private void FindPath() {

        SetPath(endWayPoint);
       
        WayPoint previous = endWayPoint.exploreFrom;
        while (previous != startWayPoint) {

            SetPath(previous);
            previous = previous.exploreFrom;
        }
        SetPath(startWayPoint);
        path.Reverse();
    }

    private void SetPath(WayPoint wayPoint) {
        path.Add(wayPoint);
        wayPoint.isPlaceable = false;
    }

    private void LoadBlockToDictionary() {

        var wayPoints = FindObjectsOfType<WayPoint>();
        foreach (var wayPoint in wayPoints) {

            Vector2Int gridPos = wayPoint.GetGridPos();
            if (worldGrid.ContainsKey(gridPos)) {
                Debug.LogWarning("Overlapping block!" + gridPos);
            }
            else {
                worldGrid.Add(gridPos, wayPoint);
            }
        }
    }


    private void BreadthFirstSearch() {

        queue.Enqueue(startWayPoint);

        while (queue.Count > 0 && isRunning) {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIFEndFound();
            FindNeighbours();
        }
    }

    private void HaltIFEndFound() {
        if (searchCenter == endWayPoint) {
            isRunning = false;
        }
    }

    private void FindNeighbours() {

        if (!isRunning) {return;}
        
        foreach (Vector2Int direction in directions) {
            Vector2Int neighbourWayPoints = searchCenter.GetGridPos() + direction;
            if (worldGrid.ContainsKey(neighbourWayPoints)) {

                QueuingNewNeighbours(neighbourWayPoints);

            }
        }
    }

    private void QueuingNewNeighbours(Vector2Int neighbourWayPoints) {
        WayPoint neighbour = worldGrid[neighbourWayPoints];

        if (neighbour.isExplored || queue.Contains(neighbour)) {  
        }
        else {
            queue.Enqueue(neighbour);
            neighbour.exploreFrom = searchCenter;
        }
        
    }
}
