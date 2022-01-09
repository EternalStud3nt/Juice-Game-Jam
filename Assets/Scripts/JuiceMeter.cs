using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuiceMeter : MonoBehaviour
{
    [SerializeField] private Image juice_UI;

    private static float juice;
    private const float maxJuice = 1000;
    public static float JuicePercent { get { return juice / maxJuice; } }

     [SerializeField] Player player;

    private void Awake()
    {
        EnemyAIController.OnDeath += AddJuice;
    }

    public static void AddJuice()
    {
        juice += 50;
    }

    private void Update()
    {
        juice_UI.fillAmount = JuicePercent;
        if(juice == maxJuice)
        {
            player.SickoMode();
        }
    }

    private void OnDisable()
    {
        EnemyAIController.OnDeath -= AddJuice;
    }
}
