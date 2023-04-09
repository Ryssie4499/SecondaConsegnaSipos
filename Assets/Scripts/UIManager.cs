using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject StartCanvas;
    public GameObject EndCanvas;
    public GameObject DefeatCanvas;
    public GameObject PauseCanvas;

    [Header("In Game UI")]
    public GameObject[] Rays;
    public Image ShieldTimer;
    public Image FreezeTimer;
    public Image MalusTimer;
    public Text enemiesDefeated;

    //References
    GameManager GM;
    Enemy e;
    PlayerMovement PM;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        PM = FindObjectOfType<PlayerMovement>();
        e = FindObjectOfType<Enemy>();
    }
    private void Update()
    {
        if (GM.gameStatus == GameStatus.gameStart)
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
        if (GM.gameStatus == GameStatus.gameEnd)
        {
            EndCanvas.SetActive(true);
        }
        if (GM.gameStatus == GameStatus.gameDefeat)
        {
            DefeatCanvas.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && GM.gameStatus == GameStatus.gameRunning)
        {
            GM.gameStatus = GameStatus.gamePaused;
            PauseCanvas.SetActive(true);
        }
        else if (GM.gameStatus == GameStatus.gamePaused && Input.GetKeyDown(KeyCode.Escape))
        {
            PLAY();
        }
    }

    public void PLAY()
    {
        StartCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        GM.gameStatus = GameStatus.gameRunning;
    }

    public void EXIT()
    {
        Application.Quit();
    }

    public void RESTART()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
