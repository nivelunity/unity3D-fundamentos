using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    
    private PlayerController myPlayerController;
    private EnemyController myEnemyController;

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
            myEnemyController = other.GetComponentInParent<EnemyController>();
            myEnemyController.StartCombat();
            FaceTransforms(transform, other.transform.parent);
            Invoke("DelayEndCombat", 2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Combates"))
        {
            if (myEnemyController == null) return;
            
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

    void DelayEndCombat()
    {
        Debug.Log("¡FIN DEL DUELO!");
        myPlayerController.EndCombat();
        myPlayerController.PlayerReset();
        myEnemyController.EndCombat();
        myEnemyController.EnemyReset();

        GameManager.Instance.Lives--;
        GameManager.Instance.UpdateLifeText();
    }

}
