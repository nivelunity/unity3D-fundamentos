using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform jugador;

    [SerializeField]
    [Range(2f,15f)]
    private float radioDeteccion = 10f;

    [SerializeField]
    [Range(45f, 180f)]
    private float fieldOfView = 90f;

    [SerializeField]
    private Animator myAnimator;

    private NavMeshAgent navMeshAgent;
    
    private float detectionRadiusSquared;

    private bool isCombat = false;
    public bool IsCombat { get => isCombat; set => isCombat = value; }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        detectionRadiusSquared = radioDeteccion * radioDeteccion;
    }

    // Update is called once per frame
    void Update()
    {

        if (isCombat) return;

        if (jugador == null) return;

        Vector3 toPlayer = jugador.position - transform.position;
        float squareDistanceToJugador = toPlayer.sqrMagnitude;

        if (squareDistanceToJugador > detectionRadiusSquared) return;
        
        float dotProduct = Vector3.Dot(transform.forward, toPlayer.normalized);
        if (dotProduct >= Mathf.Cos(fieldOfView * .5f *Mathf.Deg2Rad))
        {
            navMeshAgent.SetDestination(jugador.position);
        }

        myAnimator.SetBool("isRunning", (navMeshAgent.velocity.magnitude > 0));
    }

    public void StartCombat()
    {
        isCombat = true;
        myAnimator.SetTrigger("StartCombat");
    }

    public void EndCombat()
    {
        isCombat = false;
        myAnimator.SetTrigger("EndCombat");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
