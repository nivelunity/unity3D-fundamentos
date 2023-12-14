using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject statusCombatPanel;
    [SerializeField] private GameObject statusCombatIcon;
    [SerializeField] private GameObject livesPanel;

    public void PopulatePanel(int maxMatches)
    {
        int childCount = statusCombatPanel.transform.childCount;

        if (childCount == maxMatches)
        {
            for (int i = 0; i < maxMatches; i++)
            {
                GameObject statusIcon = statusCombatPanel.transform.GetChild(i).gameObject;
                Image iconSprite = statusIcon.GetComponent<Image>();
                iconSprite.color = Color.white;
            }
        }
        else
        {
            for (int i = 0; i < maxMatches; i++)
            {
                Debug.Log(i);
                Instantiate(statusCombatIcon, statusCombatPanel.transform);
            }
        }   
        
    }

    public void SetMatchStatusIcon(int index, int status)
    {
        GameObject statusIcon = statusCombatPanel.transform.GetChild(index).gameObject;
        Image iconSprite = statusIcon.GetComponent<Image>();

        switch (status)
        {
            case 0:
                iconSprite.color = Color.red;
                break;
            case 1:
                iconSprite.color = Color.green;
                break;
            default:
                iconSprite.color = Color.yellow;
                break;
        }
    }

    public void UpdateLives()
    {
        livesPanel.transform.GetChild((GameManager.Instance.Lives - 1)).gameObject.SetActive(false);
    }
}
