using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float initPauseTime;
    [SerializeField] private float deathTime;
    [SerializeField] private float minX, minY, maxX, maxY;
    [SerializeField] private float minimumTargetDiff = 0.5f;
    [SerializeField] private float hubRadius = 5f;
    [SerializeField] private int damage = 10;
    [SerializeField] private Transform targetPos;
    [SerializeField] private GameObject hub;
    [SerializeField] private GameObject deathParticles;

    private bool waiting;
    private bool reachedHub;

    // Start is called before the first frame update
    void Start()
    {
        hub = GameObject.FindGameObjectWithTag("Hub");
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
    }

    private void Attack()
    {
        hub.GetComponent<HubController>().TakeDamage(damage);
        Die();
    }

    private void Die()
    {
        IEnumerator Die_Cor()
        {
            // TO DO: Play Death Animation, Play Sound Effects, ...
            yield return new WaitForSeconds(deathTime);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        StartCoroutine(Die_Cor());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hub.transform.position, hubRadius);
    }
}
