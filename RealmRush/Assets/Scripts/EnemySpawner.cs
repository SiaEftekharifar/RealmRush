using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] float secondsBetweenSpawns;

    [SerializeField] GameObject enemy;

    void Start() {

        StartCoroutine(SpawnEnemies());

    }

    // Update is called once per frame
    void Update() {

    }


    IEnumerator SpawnEnemies() {
        while (true) {

           
            yield return new WaitForSeconds(secondsBetweenSpawns);
            GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
            newEnemy.transform.parent = gameObject.transform;

        }
       

    }
}
