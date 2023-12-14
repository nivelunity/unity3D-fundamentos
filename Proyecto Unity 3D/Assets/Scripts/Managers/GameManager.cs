using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    public int Lives = 3;
    public int Farms = 3;

    // Serialized TextMesh Pro components
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI farmText;

    // Initialize the singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateLifeText();
        UpdateFarmText();
    }

    // Method to update life text
    public void UpdateLifeText()
    {
        if (Lives == 0)
        {
            lifeText.text = "Game Over";
            return;
        }

        lifeText.text = "Lives: " + Lives;
    }

    // Method to update farm text
    public void UpdateFarmText()
    {
        if (Lives == 0)
        {
            lifeText.text = "You Win";
            return;
        }
        farmText.text = "Loot " + Farms+ " Farms";
    }
}
