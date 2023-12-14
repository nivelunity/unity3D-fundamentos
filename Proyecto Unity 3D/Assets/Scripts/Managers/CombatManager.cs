using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
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

    public void PlayerChoice(int newChoice)
    {
        playerChoice = (Choice)newChoice;
        enemyChoice  = (Choice)Random.Range(0, 3);
        Debug.Log(playerChoice);
        Debug.Log(enemyChoice);
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
