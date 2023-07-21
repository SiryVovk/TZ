using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material playerMat;
    [SerializeField] private Material shildMat;

    [Header ("Particles")]
    [SerializeField] private ParticleSystem destroyPlayer;
    [SerializeField] private ParticleSystem playerWinParticles;

    private MeshRenderer playerVisual;

    private Vector3 goalPosition;
    private Vector3 startPos;

    private Quaternion rotation;

    private NavMeshAgent agent;
    private LineRenderer lineRenderer;

    private string goalTag = "Goal";
    private string killZone = "Kill";

    private float lineRenderWidth = 0.15f;

    private bool hasShild = false;

    public static Action playerWin;

    public bool HasShild
    {
        get { return hasShild; }
        set 
        {
             hasShild = value;
            if (value == true)
            {
                playerVisual.material = shildMat;
            }
            else
            {
                playerVisual.material = playerMat;
            }
        }
    }

    private void Awake()
    {

        playerVisual = GetComponentInChildren<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = lineRenderWidth;
        lineRenderer.endWidth = lineRenderWidth;
        lineRenderer.positionCount = 0;

        rotation = transform.rotation;
        startPos = agent.transform.position;

        destroyPlayer.Stop();
        playerWinParticles.Stop();

        StartCoroutine(MoveToGoal());
    }

    private void Update()
    {
        if(agent.hasPath)
        {
            DrawPath();
        }
    }

    private IEnumerator MoveToGoal()
    {
        yield return new WaitForSeconds(2);

        goalPosition = GameObject.FindGameObjectWithTag(goalTag).transform.position;

        agent.SetDestination(goalPosition);
        agent.isStopped = false;
    }

    private void DrawPath()
    {
        lineRenderer.positionCount = agent.path.corners.Length;
        lineRenderer.SetPosition(0, transform.position);

        if (lineRenderer.positionCount < 2)
        {
            return;
        }

        for(int i = 0; i < agent.path.corners.Length; i++)
        {
            Vector3 position = agent.path.corners[i];
            lineRenderer.SetPosition(i,position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(goalTag))
        {
            playerWinParticles.Play();
            playerWin?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(killZone) && !hasShild)
        {
            StartCoroutine(PlayerDie());
        }
    }

    private IEnumerator PlayerDie()
    {
        playerVisual.enabled = false;
        destroyPlayer.Play();
        agent.isStopped = true;
        agent.ResetPath();
        lineRenderer.positionCount = 0;

        yield return new WaitForSeconds(2);

        destroyPlayer.Stop();
        playerVisual.enabled = true;


        agent.Warp(startPos);
        transform.position = startPos;
        transform.rotation = rotation;

        StartCoroutine(MoveToGoal());
    }
}
