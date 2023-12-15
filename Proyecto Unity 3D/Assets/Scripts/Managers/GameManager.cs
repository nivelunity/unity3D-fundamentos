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
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI farmText;

    [SerializeField] private Sprite[] combatIcons;
    public Sprite[] CombatIcons { get => combatIcons;}


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
        UpdateFarmText();
    }

    // Method to update life text
    public void UpdateLifeText()
    {
        if (Lives == 0)
        {
            statusText.gameObject.SetActive(true);
            statusText.text = "Game Over";
            return;
        }
    }

    // Method to update farm text
    public void UpdateFarmText()
    {
        if (Farms == 0)
        {
            statusText.gameObject.SetActive(true);
            statusText.text = "You Win";
        }

        farmText.text = "Loot " + Farms+ " Farms";
    }
}
