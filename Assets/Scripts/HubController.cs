using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HubController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private Image hpBar;

    [Header("Other")]
    [SerializeField] public AudioClip damageSound;
    [SerializeField] public float hubRadius = 5f;
    [SerializeField] private int initHealth;
    private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        health = initHealth;
        endGamePanel.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if(health > 0)
        {
            health -= damage;
            SFXPlayer.Instance.PlaySFX(damageSound);
        }
        else
        {
            EndGame();
        }
        hpBar.fillAmount = (float)health / initHealth;
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
