using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

    [SerializeField]
    private Image combatIcon;

    [SerializeField]
    private Collider CombatTrigger;

    private NavMeshAgent navMeshAgent;
    
    private float detectionRadiusSquared;

    private bool isCombat = false;
    public bool IsCombat { get => isCombat; set => isCombat = value; }

    Vector3 initPosition;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        detectionRadiusSquared = radioDeteccion * radioDeteccion;
        initPosition = transform.position;
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
        CombatTrigger.enabled = false;
    }

    public void EndCombat()
    {
        myAnimator.SetTrigger("EndCombat");
        combatIcon.gameObject.SetActive(false);
    }

    public void ChoiceCombat()
    {
        myAnimator.SetTrigger("Attack");
    }

    public void EnemyDeath()
    {
        myAnimator.SetTrigger("Death");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }


    public void EnemyReset()
    {
        isCombat = false;
        navMeshAgent.ResetPath();
        transform.position = initPosition;
        myAnimator.SetTrigger("ResetCombat");
        myAnimator.SetBool("isRunning", false);
        initPosition = transform.position;
        CombatTrigger.enabled = true;
    }

    public void SetEnemyCombatIcon(Sprite newIcon)
    {
        if (!combatIcon.gameObject.activeSelf)
        {
            combatIcon.gameObject.SetActive(true);
        }

        combatIcon.sprite = newIcon;
    }
}
