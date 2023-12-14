using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    public UnityEvent OnEndCombat;

    [SerializeField]
    [Range(1,5)]
    private int maxMatches = 3;
        
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

    public void PlayerChoice(int newChoice)
    {
        playerChoice = (Choice)newChoice;
        enemyChoice  = (Choice)Random.Range(0, 3);

        matches[played] = Match();
       
        Debug.Log(playerChoice);
        Debug.Log(enemyChoice);
        Debug.Log(matches[played]);

        played++;

        if (played == maxMatches)
        {
            OnEndCombat.Invoke();
            played = 0;
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
}
