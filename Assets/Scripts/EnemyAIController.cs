using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{

    public float speed;
    public float initPauseTime;
    public float minX, minY, maxX, maxY;
    public float changeDirDist = 0.5f;
    public float hubRadius = 5f;
    public float attackTime = 5f;

    public Transform targetPos;

    private Rigidbody2D rb;

    public int damage = 10;

    bool waiting;
    bool attacking;


    private GameObject hub;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hub = GameObject.FindGameObjectWithTag("Hub");
        targetPos.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, hub.transform.position) < hubRadius)
        {
            if(!attacking)
                StartCoroutine(DealDamage());
        }
       
        // if enemy has arrived at the random location
        if (Vector2.Distance(transform.position, targetPos.position) < changeDirDist)
        {
            if (!waiting)
                StartCoroutine(ChooseNewTargetPos());
        }
        
        
    }

    IEnumerator ChooseNewTargetPos()
    {
        waiting = true;
        yield return new WaitForSeconds(initPauseTime);
        targetPos.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        waiting = false;
    }

    IEnumerator DealDamage()
    {
        attacking = true;
        yield return new WaitForSeconds(attackTime);
        attacking = false;
    }

}
