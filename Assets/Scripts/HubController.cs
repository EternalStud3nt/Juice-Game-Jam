using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject endGamePanel;

    [Header("Other")]
    [SerializeField] public float hubRadius = 5f;
    [SerializeField] private int initHealth;
    private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        health = initHealth;
        endGamePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if(health > 0)
        {
            health -= damage;
        }
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        //Play Sounds, End Screen etc.
        GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>().spawnEnemies = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().takeInput = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        endGamePanel.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMainMenu()
    {
        //Load Main Menu Scene
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hubRadius);
    }
}
