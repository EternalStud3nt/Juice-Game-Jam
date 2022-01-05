using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{

    public float speed;
    public float initPauseTime;
    public Transform targetPos;
    public float minX, minY, maxX, maxY;
    public float changeDirDist = 0.5f;
    bool waiting;

    // Start is called before the first frame update
    void Start()
    {
        targetPos.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);

        // if enemy has arrived at the random location
        if (Vector2.Distance(transform.position, targetPos.position) < changeDirDist)
        {
            if(!waiting)
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
}
