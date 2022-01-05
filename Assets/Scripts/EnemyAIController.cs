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

    private float PauseTime;


    // Start is called before the first frame update
    void Start()
    {
        PauseTime = initPauseTime;
        targetPos.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPos.position) < changeDirDist)
        {
            targetPos.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            PauseTime = initPauseTime;
        }
        else
        {
            PauseTime -= Time.deltaTime;
            
        }
            
        
    }
}
