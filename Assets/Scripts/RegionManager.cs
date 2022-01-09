using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionManager : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private float tolTime;

    bool switched;

    private void Awake()
    {
        Player.OnSwitch += setSwitched;
    }

    public void setSwitched()
    {
        IEnumerator setSwitched_Cor()
        {
            switched = true;
            yield return new WaitForSeconds(tolTime);
            switched = false;
        }
        StartCoroutine(setSwitched_Cor());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Pilot") && switched) 
        {
            //Restart Game or Teleport back + Scene Transition?
            player.Respawn();
        }
    }

    private void OnDisable()
    {
        Player.OnSwitch -= setSwitched;
    }


}
