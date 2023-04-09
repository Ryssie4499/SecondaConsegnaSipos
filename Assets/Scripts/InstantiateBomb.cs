using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBomb : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject bomb;                     //inserisco da Inspector il prefab della bomba da spawnare

    //References
    GameManager GM;                             //dal GameManager riprendo lo status di gioco in modo che non sia possibile istanziare bombe mentre � attivo il men� di pausa
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GM.gameStatus == GameStatus.gameRunning)    //se clicco il tasto space e lo status di gioco � in running istanzio una bomba e arrotondo la sua posizione all'intero pi� vicino
        {
            Instantiate(bomb, new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), Quaternion.identity);
        }
    }
}
