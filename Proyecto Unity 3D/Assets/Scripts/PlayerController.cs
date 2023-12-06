using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 25f)]
    private float speed = 10f;

    [SerializeField]
    private TextMeshProUGUI countText;

    [SerializeField]
    private GameObject winTextObject;

    [SerializeField]
    [Range(2, 40)]
    private int objetivosGemas;


    private Rigidbody rb;

    private float movementX;
    private float movementY;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        if (movement.magnitude == 0) return;

        Quaternion rotation = Quaternion.LookRotation(movement);
        rb.MoveRotation(rotation);
        rb.AddForce(movement * speed);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Combates"))
        {
            Debug.Log("�FIN DEL DUELO!");
            winTextObject.GetComponent<TextMeshProUGUI>().text = "FIN DE DUELO";
            Invoke("DesactivarConDelay", 1f);
        }

        if (other.gameObject.CompareTag("Coleccionable"))
        {
            Debug.Log("�FIN DEL LA CENA!");
            winTextObject.GetComponent<TextMeshProUGUI>().text = "NO MAS MANZANAS";
            Invoke("DesactivarConDelay", 1f);
        }
    }

    void DesactivarConDelay()
    {
        winTextObject.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Combates"))
        {
            Debug.Log("�ES HORA DEL DUELO!");
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "EL DUELO";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("EN TRIGGER");
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("�COMIENDO MANZANA !");
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "COMIENDO";
        }
    }

    void SetCountText()
    {
        countText.text = "Gemas: " + count.ToString();
    }
}
