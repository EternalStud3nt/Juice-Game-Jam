using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float initPauseTime;
    [SerializeField] private float minX, minY, maxX, maxY;
    [SerializeField] private float minimumTargetDiff = 0.5f;
    [SerializeField] private Transform targetPos;
    private bool waiting;

    [Header("Hub")]
    [SerializeField] private float deathTime;
    [SerializeField] private float hubRadius = 5f;
    [SerializeField] private int damage = 10;
    [SerializeField] private GameObject hub;
    [SerializeField] private GameObject deathParticles;
    private bool reachedHub;

    [Header("Other")]
    [SerializeField] private GameObject enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        hub = GameObject.FindGameObjectWithTag("Hub");
        targetPos.SetParent(null, true);
        targetPos.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!reachedHub)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);
            if (!waiting)
            {
                if (Vector2.Distance(transform.position, hub.transform.position) <= hubRadius) // Within hub radius
                {
                    TargetHub();
                }
                else if (Vector2.Distance(transform.position, targetPos.position) < minimumTargetDiff) // if we reached our target
                {
                    TargetNewRandomPos();
                }
            }
        }
    }

    private void TargetNewRandomPos()
    {
        IEnumerator TargetNewRandomPos_Cor()
        {
            waiting = true;
            yield return new WaitForSeconds(initPauseTime);
            targetPos.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            waiting = false;
        }
        StartCoroutine(TargetNewRandomPos_Cor());
    }

    private void TargetHub()
    {
        targetPos.position = hub.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Hub"))
        {
            reachedHub = true;
            Attack();
        }
        if (collision.collider.CompareTag("Pilot"))
        {
            Debug.Log("I found a pilot");
            Die(true);
        }
    }

    private void Attack()
    {
        hub.GetComponent<HubController>().TakeDamage(damage);
        Die();
    }

    public void Die(bool instant = false)
    {
        IEnumerator Die_Cor()
        {
            // TO DO: Play Death Animation, Play Sound Effects, ...
            yield return new WaitForSeconds(deathTime);
            enemySpawner.GetComponent<EnemySpawner>().decAICount();
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if(!instant)
            StartCoroutine(Die_Cor());
        else
        {
            enemySpawner.GetComponent<EnemySpawner>().decAICount();
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hub.transform.position, hubRadius);
    }
}
