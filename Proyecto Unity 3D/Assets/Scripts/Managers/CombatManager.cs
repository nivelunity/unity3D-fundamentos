using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    public UnityEvent OnEndCombat;
    public UnityEvent OnWinCombat;
    public UnityEvent OnLoseCombat;

    [Serializable]
    public class TwoIntEvent : UnityEvent<int, int> { }
    public TwoIntEvent OnPlayerChoice;

    [Serializable]
    public class IntEvent : UnityEvent<int> { }
    public IntEvent OnResetMatches;

    [SerializeField]
    [Range(1, 5)]
    private int maxMatches = 3;

    [SerializeField]
    [Range(1, 5)]
    private int delayNextCombat = 3;

    private enum Choice
    {
        Rock,
        Paper,
        Scissors
    }

    private enum MatchState
    {
        Lose,
        Win,
        Draw
    }

    private Choice playerChoice;
    private Choice enemyChoice;

    private int played = 0;
    private MatchState[] matches;

    private void Start()
    {
        matches = new MatchState[maxMatches];
    }

    public void InitCombatConfig()
    {
        OnResetMatches.Invoke(maxMatches);
    }

    public void PlayerChoice(int newChoice)
    {
        playerChoice = (Choice)newChoice;
        enemyChoice = (Choice)UnityEngine.Random.Range(0, 3);

        matches[played] = Match();

        Debug.Log(playerChoice);
        Debug.Log(enemyChoice);
        Debug.Log(matches[played]);

        OnPlayerChoice.Invoke(played, (int)matches[played]);
        played++;

        if (played == maxMatches)
        {
            OnEndCombat.Invoke();
            Invoke("DelayNextCombat", delayNextCombat);
        }
    }

    private MatchState Match()
    {
        if (playerChoice == enemyChoice)
        {
            return MatchState.Draw;
        }
        else if (IsWinCondition(Choice.Rock, Choice.Scissors) ||
                 IsWinCondition(Choice.Paper, Choice.Rock) ||
                 IsWinCondition(Choice.Scissors, Choice.Paper))
        {
            return MatchState.Win;
        }
        else
        {
            return MatchState.Lose;
        }
    }

    private bool IsWinCondition(Choice winPlayerChoice, Choice loseEnemyChoice)
    {
        return playerChoice == winPlayerChoice && enemyChoice == loseEnemyChoice;
    }

    public bool IsWinCombat()
    {
        int winCount = 0;
        int loseCount = 0;

        for (int i = 0; i < matches.Length; i++)
        {
            if (matches[i] == MatchState.Draw) continue;

            if (matches[i] == MatchState.Win)
            {
                winCount++;
            }
            else
            {
                loseCount++;
            }
        }
        return winCount > loseCount;
    }

    void DelayNextCombat()
    {
        if (IsWinCombat())
        {
            OnWinCombat.Invoke();
        }
        else
        {
            GameManager.Instance.Lives--;
            OnLoseCombat.Invoke();
        }
        played = 0;
    }
}
