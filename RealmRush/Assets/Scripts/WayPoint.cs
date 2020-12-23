﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class WayPoint : MonoBehaviour {
    // Vector2Int gridPos;
    const int gridSize = 10;

    public bool isExplored = false;
    public WayPoint exploreFrom;
    public bool isPlaceable = true;


    [SerializeField] GameObject towerPrefab;
    [SerializeField] Transform towerParent;

    public int GetGridSize() {
        return gridSize;
    }

    public Vector2Int GetGridPos() {

        return new Vector2Int (
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize));
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0) && isPlaceable) {
           GameObject tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
           tower.transform.parent = towerParent;
           isPlaceable = false;
        }
    }
}
