using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubController : MonoBehaviour
{
    public int initHealth;

    public int health = 100;

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
        health -= damage;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy")){
            //TO DO: "Bounce-off" Effect
        }
    }
}
