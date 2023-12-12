using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    
    private PlayerController myPlayerController;

    private void Awake()
    {
        myPlayerController = GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Combates"))
        {
            Debug.Log("¡ES HORA DEL DUELO!");
            myPlayerController.StartCombat();
            Debug.Log(other.gameObject.name);
            other.GetComponentInParent<EnemyController>().StartCombat();
            FaceTransforms(transform, other.transform.parent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Combates"))
        {
            Debug.Log("¡FIN DEL DUELO!");
            myPlayerController.EndCombat();
            other.GetComponentInParent<EnemyController>().EndCombat();
        }
    }

    void FaceTransforms(Transform playerTransform, Transform enemyTransform)
    {
        // Calculate the direction from the first transform to the second transform
        Vector3 direction = enemyTransform.position - playerTransform.position;

        // Get the quaternion that looks in that direction
        Quaternion rotation = Quaternion.LookRotation(direction);
        playerTransform.rotation = rotation;
        enemyTransform.rotation  = Quaternion.LookRotation(-direction);
    }
}
