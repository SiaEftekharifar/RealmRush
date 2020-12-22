using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(WayPoint))]


public class CubeEditor : MonoBehaviour {

    WayPoint wayPoint;

    private void Awake() {

        wayPoint = GetComponent<WayPoint>();

    }

    void Update() {

        SnapToGrid();
        RenameCubes();

    }


    private void SnapToGrid() {

        int gridSize = wayPoint.GetGridSize();
       
        transform.position = new Vector3(wayPoint.GetGridPos().x * gridSize
                                         ,0f
                                         ,wayPoint.GetGridPos().y * gridSize);

    }

    private void RenameCubes() {

        int gridSize = wayPoint.GetGridSize();
        TextMesh textMesh = GetComponentInChildren<TextMesh>();

        textMesh.text = (wayPoint.GetGridPos().x).ToString()
                        + "," +
                        (wayPoint.GetGridPos().y).ToString();
        string cubeName = textMesh.text;
        gameObject.name = cubeName;
    }
}
