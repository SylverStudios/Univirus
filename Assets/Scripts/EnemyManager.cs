using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    public Text resultText;
    public GameObject enemies;
    public GameObject enemy;
    public GameObject enemyCollider;
    public Transform[] spawnPoints;

    void Update() {
        if (enemies.transform.childCount < 12)
        {
            Spawn();
        }
    }

    void Spawn() {
        if (resultText.text != "") { return; }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        GameObject newEnemy =
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        newEnemy.transform.parent = enemies.transform;

        GameObject newEnemyCollider =
            Instantiate(enemyCollider, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        newEnemyCollider.transform.parent = newEnemy.transform;
    }
}