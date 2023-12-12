using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Combates"))
        {
            Debug.Log("¡ES HORA DEL DUELO!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Combates"))
        {
            Debug.Log("¡FIN DEL DUELO!");

        }
    }
}
