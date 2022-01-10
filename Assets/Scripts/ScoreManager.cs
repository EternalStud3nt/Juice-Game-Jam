using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int minScoreFac;
    [SerializeField] private int maxScoreFac;
    [SerializeField] private Animator textAnim;

    public Text highScoreText;

    private int score;
    


    private void Awake()
    {
        EnemyAIController.OnDeath += AddScore;
        Player.OnStart += EnableHUD;
    }

    // Start is called before the first frame update
    void Start()
    {
        highScoreText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        highScoreText.text = score.ToString();
    }


    private void AddScore()
    {
        textAnim.SetTrigger("increaseScore");
        score += Random.Range(minScoreFac, maxScoreFac);
    }

    private void EnableHUD()
    {
        highScoreText.enabled = true;
    }

    private void OnDisable()
    {
        EnemyAIController.OnDeath -= AddScore;
        Player.OnStart -= EnableHUD;
    }
}
