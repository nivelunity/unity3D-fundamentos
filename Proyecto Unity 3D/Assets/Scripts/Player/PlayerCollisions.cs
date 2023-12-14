using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollisions : MonoBehaviour
{
    public UnityEvent OnStartCombat;

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
            if (myEnemyController != null) return;

            OnStartCombat.Invoke();
            myPlayerController.StartCombat();
            myEnemyController = other.GetComponentInParent<EnemyController>();
            myEnemyController.StartCombat();
            FaceTransforms(transform, other.transform.parent);
        }
    }

    void FaceTransforms(Transform playerTransform, Transform enemyTransform)
    {
        // Calculate the direction from the first transform to the second transform
        Vector3 direction = enemyTransform.position - playerTransform.position;
        // Get the quaternion that looks in that direction
        Quaternion rotation = Quaternion.LookRotation(direction);
        playerTransform.rotation = rotation;
        enemyTransform.rotation = Quaternion.LookRotation(-direction);
    }

    public void DelayEndCombat()
    {
        Debug.Log("¡FIN DEL DUELO!");

        myPlayerController.EndCombat();
        myPlayerController.PlayerReset();
        myEnemyController.EndCombat();
        myEnemyController.EnemyReset();

        myEnemyController = null;

        GameManager.Instance.Lives--;
        GameManager.Instance.UpdateLifeText();
    }

}
