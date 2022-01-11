using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class JuiceMeter : MonoBehaviour
{
    [SerializeField] private Image juice_UI;

    private static float juice;
    private const float maxJuice = 1000;
    public static float JuicePercent { get { return juice / maxJuice; } }
    public static Action OnSickoMode;
    private new static bool enabled;

    [SerializeField] Player player;

    private void Awake()
    {
        EnemyAIController.OnDeath += AddJuice;
        Player.OnStart += Enable;
        gameObject.SetActive(false);
    }

    private void Enable()
    {
        gameObject.SetActive(true);
    }

    public static void AddJuice()
    {
        juice += 50;
    }

    private void Update()
    {
        juice_UI.fillAmount = JuicePercent;
        print(JuicePercent);
        if(juice >= maxJuice && !enabled)
        {
            player.SickoMode();
            OnSickoMode?.Invoke();
            enabled = true;
        }
       
    }

    public static void ResetJuice()
    {
        juice = 0;
        enabled = false;
    }

    private void OnDestroy()
    {
        EnemyAIController.OnDeath -= AddJuice;
        Player.OnStart -= Enable;
    }
}
