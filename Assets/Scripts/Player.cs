using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform pilot1;
    [SerializeField] private Transform pilot2;
    [SerializeField] private float rotationSpeed;

    private Vector3 RotationAxis { get { return currentPilot.forward; } }
    private Transform currentPilot;

    private void Start()
    {
        currentPilot = pilot1;
    }

    private void SwitchPilot()
    {
        if(currentPilot == pilot1)
        {
            currentPilot = pilot2;
            Debug.Log("Switching to pilot2");
        }
        else if (currentPilot == pilot2)
        {
            currentPilot = pilot1;
            Debug.Log("Switching to pilot1");
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SwitchPilot();
        }
    }

    private void FixedUpdate()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        transform.Rotate(RotationAxis, rotationSpeed * Time.deltaTime);
    }
}
