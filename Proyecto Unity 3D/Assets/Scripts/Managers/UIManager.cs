using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject statusCombatPanel;
    [SerializeField] private GameObject statusCombatIcon;

    private void Start()
    {
        PopulatePanel(3);
        SetMatchStatusIcon(0, 0);
        SetMatchStatusIcon(1, 1);
        SetMatchStatusIcon(2, 2);
    }

    public void PopulatePanel(int maxMatches)
    {
        int childCount = statusCombatPanel.transform.childCount;

        if (childCount == maxMatches) return;

        for (int i = 0; i < maxMatches; i++)
        {
            Debug.Log(i);
            Instantiate(statusCombatIcon, statusCombatPanel.transform);
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
                iconSprite.color = Color.gray;
                break;
        }
    }
}
