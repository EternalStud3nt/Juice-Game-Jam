using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform pilot1;
    [SerializeField] private Transform pilot2;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxDistanceBetweenPilots = 3;
    [SerializeField] private float pullPilotSpeed = 10;

    private Transform currentPilot;
    private Transform otherPilot;
    private bool pulling;
    Vector2 distanceBetweenPilots;
    private float rotationRadius;

    private void Start()
    {
        currentPilot = pilot1;
        otherPilot = pilot2;
        rotationRadius = maxDistanceBetweenPilots;
    }

    private void SwitchPilot()
    {
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
            JuiceMeter.AddJuice(1);
        }
    }

    private void PullOtherPilot()
    {
        pulling = true;
        rotationRadius -= pullPilotSpeed * Time.deltaTime;
    }

    private void PushOtherPilot()
    {
        print("Pulling");
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pilot1.position, maxDistanceBetweenPilots);
    }
}
