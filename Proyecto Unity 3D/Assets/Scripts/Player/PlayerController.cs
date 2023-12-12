using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 25f)]
    private float speed = 10f;

    [SerializeField]
    LayerMask IgnoreMe;

    [SerializeField]
    private Animator myAnimator;

    private Rigidbody rb;
    private Vector3 targetPos;
    private bool isMoving = false;
    private bool isCombat = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isCombat) return;

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit; // Define variable to hold raycast hit information

            // Check if raycast hits an object
            if (Physics.Raycast(ray, out hit, 1000f, ~IgnoreMe))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    targetPos = hit.point; // Set target position
                    isMoving = true; // Start player movement
                }
            }
            else
            {
                isMoving = false;
            }
        }
        myAnimator.SetBool("isRunning", isMoving);
    }

    private void FixedUpdate()
    {
        if (isCombat) return;

        Vector3 directionToMove = targetPos - rb.position;
        float distanceToMoveSquared = directionToMove.sqrMagnitude;

        directionToMove.Normalize();

        Quaternion rotation = Quaternion.LookRotation(directionToMove);
        Quaternion yRotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        rb.MoveRotation(yRotation);


        if (distanceToMoveSquared < 0.04f)
        {
            isMoving = false;
            rb.Sleep();
            return;
        }

        if (isMoving)
        {
            rb.AddForce(directionToMove * speed);
        }
    }

    public void StartCombat()
    {
        isCombat = true;
        myAnimator.SetTrigger("StartCombat");
        rb.Sleep();
    }

    public void EndCombat()
    {
        isCombat = false;
        myAnimator.SetTrigger("EndCombat");
    }
}
