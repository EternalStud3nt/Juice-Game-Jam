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
    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private float minRotationSpeed;
    [SerializeField] Vector3 minScale;
    [SerializeField] Vector3 maxScale;
    [SerializeField] private float sickomodeTimer;
    [SerializeField] private float maxDistanceBetweenPilots = 3;
    [SerializeField] private float pullPilotSpeed = 10;
    [SerializeField] Cinemachine.CinemachineVirtualCamera virCam;
    [SerializeField] private Animator camAnim;
    [SerializeField] private GameObject mainMenuUI;

    public bool takeInput;
    public static Action<Transform> OnSwitch;
    public static Action OnStart;

    private Transform currentPilot;
    private Transform otherPilot;
    private bool pulling;
    private bool startGame;
    Vector2 distanceBetweenPilots;
    private float rotationRadius;

    private void Awake()
    {
        EnemyAIController.OnDeath += Screenshake;
    }

    private void Start()
    {
        mainMenuUI.SetActive(true);
        startGame = true;
        takeInput = true;
        currentPilot = pilot1;
        otherPilot = pilot2;
        rotationRadius = maxDistanceBetweenPilots;
        currentPilot.position = spawnPoint.position;
        rotationSpeed = minRotationSpeed;
        OnSwitch.Invoke(currentPilot);
    }

    private void SwitchPilot()
    {
        Transform oldPilot = currentPilot;
        currentPilot = otherPilot;
        otherPilot = oldPilot;
        OnSwitch.Invoke(currentPilot);
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
                if (startGame)
                {
                    mainMenuUI.SetActive(false);
                    OnStart?.Invoke();
                    camAnim.SetTrigger("zoomOut");
                    startGame = false;
                }
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

    private void Screenshake()
    {
        camAnim.SetTrigger("shakeCam");
    }

    public void SickoMode()
    {
        IEnumerator SickoMode_Cor()
        {
            rotationSpeed = maxRotationSpeed;
            pilot1.localScale = maxScale;
            pilot2.localScale = maxScale;
            yield return new WaitForSeconds(sickomodeTimer);
            pilot1.localScale = minScale;
            pilot2.localScale = minScale;
            rotationSpeed = minRotationSpeed;
        }
        StartCoroutine(SickoMode_Cor());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pilot1.position, maxDistanceBetweenPilots);
    }

    private void OnDisable()
    {
        EnemyAIController.OnDeath -= Screenshake;
    }
}
