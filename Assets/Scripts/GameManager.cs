using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    gamePaused,
    gameRunning,
    gameEnd,
    gameStart,
    gameDefeat
}
public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool ray;
    [HideInInspector] public bool shield;
    [HideInInspector] public bool freeze;
    [HideInInspector] public bool malus;
    
    [Header("Status")]
    public GameStatus gameStatus;

    [Header("Enemies")]
    public float numOfDefeated;
    
    private void Update()
    {
        if(numOfDefeated == 10f && gameStatus == GameStatus.gameRunning)
        {
            gameStatus = GameStatus.gameEnd;
        }
    }
}
