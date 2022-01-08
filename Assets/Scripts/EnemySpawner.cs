using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private float minX, maxX, minY, maxY;
    [SerializeField] private float spawnPause;
    [SerializeField] private int enemyCount = 0;
    [SerializeField] private GameObject[] enemies;

    public bool spawnEnemies;

    private int ind;
    private Vector2 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemies = true;
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
            incAICount();
            yield return new WaitForSeconds(spawnPause);
            SpawnEnemy();
        }
        if(enemyCount < 60 && spawnEnemies)
        {
            StartCoroutine(SpawnEnemy_Cor());
        }
        else
        {
            Debug.Log("Too many entities created");
        }
        
    }

    private void incAICount()
    {
        enemyCount++;
    }

    public void decAICount()
    {
        enemyCount--;
    }
}
