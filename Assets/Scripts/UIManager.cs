using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject StartCanvas;
    public GameObject[] Rays;
    public Image ShieldTimer;
    public Image FreezeTimer;
    public Image MalusTimer;
    public Text enemiesDefeated;
    GameManager GM;
    Enemy e;
    PlayerMovement PM;
    Bomb b;
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        PM = FindObjectOfType<PlayerMovement>();
        e = FindObjectOfType<Enemy>();
        b = FindObjectOfType<Bomb>();
    }
    private void Update()
    {
        if(GM.gameStatus == GameStatus.gameStart)
        {
            StartCanvas.SetActive(true);
        }

        if (GM.gameStatus == GameStatus.gameRunning)
        {
            enemiesDefeated.text = GM.numOfDefeated + "/10";
            if (GM.shield == true)
            {
                ShieldTimer.fillAmount -= 1 / (PM.shieldTimer + 2) * Time.deltaTime;
            }
            if (GM.malus == true)
            {
                MalusTimer.fillAmount -= 1 / (e.malusTime + 3) * Time.deltaTime;
            }
            if (GM.freeze == true)
            {
                FreezeTimer.fillAmount -= 1 / (e.freezingTime + 2) * Time.deltaTime;
            }
            if (GM.ray == false)
            {
                foreach (GameObject ray in Rays)
                {
                    ray.SetActive(false);
                }
            }
            else if (GM.ray == true)
            {
                foreach (GameObject ray in Rays)
                {
                    ray.SetActive(true);
                }
            }

        }
    }
    public void PLAY()
    {
        StartCanvas.SetActive(false);
        GM.gameStatus = GameStatus.gameRunning;
    }

    public void EXIT()
    {
        Application.Quit();
    }
}
