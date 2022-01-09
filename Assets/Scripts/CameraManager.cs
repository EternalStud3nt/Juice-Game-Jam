using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    Camera cam;
    Transform target;
    Player player;

    private void Awake()
    {
        Player.OnSwitch += changeTarget;
    }
    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.main;
        
    }

    private void LateUpdate()
    {
        cam.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
    }

    private void changeTarget(Transform target)
    {
        this.target = target;

    }

    private void OnDisable()
    {
        Player.OnSwitch -= changeTarget;
    }

}
