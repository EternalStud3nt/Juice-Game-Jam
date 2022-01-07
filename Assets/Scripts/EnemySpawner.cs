using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float minX, maxX, minY, maxY;
    [SerializeField] private float spawnPause;

    private int ind;
    private Vector2 spawnPoint;

    [SerializeField] private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        ind = Random.Range(0, enemies.Length);
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        IEnumerator SpawnEnemy_Cor()
        {
            spawnPoint.x = Random.Range(minX, maxX);
            spawnPoint.y = Random.Range(minY, maxY);
            Instantiate(enemies[ind], spawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(spawnPause);
            SpawnEnemy();
        }
        StartCoroutine(SpawnEnemy_Cor());
    }
}
