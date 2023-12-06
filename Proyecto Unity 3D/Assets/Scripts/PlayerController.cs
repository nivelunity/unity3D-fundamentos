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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " OnTriggerEnter con " + other.gameObject.name);

        if (other.gameObject.CompareTag("Combates"))
        {
            Debug.Log("¡ES HORA DEL DUELO!");
        }

        if (other.gameObject.CompareTag("Coleccionable"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            if (count >= objetivosGemas)
            {
                winTextObject.SetActive(true);
                Destroy(GameObject.FindGameObjectWithTag("Enemigos"));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + " OnCollisionEnter con " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemigos"))
        {
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "GAME OVER";
        }
    }

    void SetCountText()
    {
        countText.text = "Gemas: " + count.ToString();
    }
}
