using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBomb : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject bomb;

    //References
    GameManager GM;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GM.gameStatus == GameStatus.gameRunning)
        {
            Instantiate(bomb, new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), Quaternion.identity);
        }
    }
}
