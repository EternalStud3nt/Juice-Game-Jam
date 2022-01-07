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
    
    public static void AddJuice(float amount)
    {
        juice += amount;
    }

    private void Update()
    {
        juice_UI.fillAmount = JuicePercent;
    }
}
