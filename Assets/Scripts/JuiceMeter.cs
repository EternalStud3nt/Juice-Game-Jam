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
    }

    private void OnDisable()
    {
        EnemyAIController.OnDeath -= AddJuice;
    }
}
