using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject jugador;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - jugador.transform.position;
    }
      
    private void LateUpdate()
    {
        transform.position = jugador.transform.position + offset;
    }
}
