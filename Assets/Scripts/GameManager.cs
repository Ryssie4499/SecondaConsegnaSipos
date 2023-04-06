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
    public Transform player;
    public float boundaryHeight;
    public float boundaryWidth;
    [HideInInspector] public Vector2 pOldPos;
    [HideInInspector] public bool ray;
    [HideInInspector] public bool shield;
    [HideInInspector] public bool freeze;
    [HideInInspector] public bool malus;
    [HideInInspector] public float weaponRange = 1.2f;
    public float numOfDefeated;
    public GameStatus gameStatus;
    
    private void Update()
    {
        if(numOfDefeated == 10f && gameStatus == GameStatus.gameRunning)
        {
            gameStatus = GameStatus.gameEnd;
        }
    }
}
