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

    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jugador == null) return;

        float distance = Vector3.Distance(transform.position, jugador.position);


        Vector3 toPlayer = (jugador.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, toPlayer);

        if (distance <= radioDeteccion && (angleToPlayer <= fieldOfView * 0.5f))
        {
            navMeshAgent.SetDestination(jugador.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
