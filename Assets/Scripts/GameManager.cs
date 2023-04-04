using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [HideInInspector] public float weaponRange = 1f;
    public float numOfDefeated;
    private void Update()
    {
        if(numOfDefeated == 10f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
