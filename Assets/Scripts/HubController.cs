using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubController : MonoBehaviour
{
    [SerializeField] private int initHealth;
    private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        health = initHealth;
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
        Debug.Log("Game Over!");
        //Reset Properties

    }
}
