using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform pilot1;
    [SerializeField] private Transform pilot2;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxDistanceBetweenPilots = 3;
    [SerializeField] private float pullPilotSpeed = 10;
    [SerializeField] Cinemachine.CinemachineVirtualCamera virCam;

    public bool takeInput;
    public static Action OnSwitch;

    private Transform currentPilot;
    private Transform otherPilot;
    private bool pulling;
    Vector2 distanceBetweenPilots;
    private float rotationRadius;

    private void Start()
    {
        takeInput = true;
        currentPilot = pilot1;
        otherPilot = pilot2;
        rotationRadius = maxDistanceBetweenPilots;
        currentPilot.position = spawnPoint.position;
    }

    private void SwitchPilot()
    {
        OnSwitch.Invoke();
        Transform oldPilot = currentPilot;
        currentPilot = otherPilot;
        otherPilot = oldPilot;
        rotationSpeed *= -1;
    }

    private void Update()
    {
        distanceBetweenPilots = currentPilot.position - otherPilot.position;

        HandleInput();

        if (distanceBetweenPilots.magnitude < maxDistanceBetweenPilots && !pulling)
        {
            PushOtherPilot();
        }

        lineRenderer.SetPosition(0, currentPilot.position);
        lineRenderer.SetPosition(1, otherPilot.position);
    }

    private void HandleInput()
    {
        if (takeInput)
        {
            if (Input.GetButtonDown("Jump"))
            {
                SwitchPilot();
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                if (distanceBetweenPilots.magnitude > 0.5f)
                {
                    PullOtherPilot();
                }
            }
            else if (!Input.GetKey(KeyCode.LeftShift))
            {
                pulling = false;
            }
            if (Input.GetMouseButton(0))
            {
                JuiceMeter.AddJuice();
            }
        }
        
    }

    private void PullOtherPilot()
    {
        pulling = true;
        rotationRadius -= pullPilotSpeed * Time.deltaTime;
    }

    private void PushOtherPilot()
    {
        rotationRadius += pullPilotSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        otherPilot.position = currentPilot.position + new Vector3(rotationRadius * Mathf.Cos(Time.time * Time.deltaTime * rotationSpeed), rotationRadius * Mathf.Sin(Time.time * Time.deltaTime * rotationSpeed)) ;
    }

    public void Respawn()
    {
        currentPilot.position = spawnPoint.position;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pilot1.position, maxDistanceBetweenPilots);
    }
}
