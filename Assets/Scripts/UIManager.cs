using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject StartCanvas;      //men� di inizio con play ed exit
    public GameObject EndCanvas;        //men� di vittoria con la possibilit� di ricominciare o uscire
    public GameObject DefeatCanvas;     //men� di sconfitta con la possibilit� di ritentare o uscire
    public GameObject PauseCanvas;      //men� di pausa con la possibilit� di riprendere il gioco, ricominciarlo o uscire

    [Header("In Game UI")]
    public GameObject[] Rays;           //i raggi visibili in alto a sinistra che si illuminano quando il MaxRay � attivo
    public Image ShieldTimer;           //il timer dello scudo
    public Image FreezeTimer;           //il timer del freeze
    public Image MalusTimer;            //il timer del malus
    public Text enemiesDefeated;        //il contatore di nemici sconfitti che si aggiorna quando una bomba colpisce un nemico

    //References
    GameManager GM;                     //ricerco il numero di nemici sconfitti e lo status di gioco
    Enemy e;                            //richiamer� i timer del malus e del freeze
    PlayerMovement PM;                  //richiamer� il timer dello scudo

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        PM = FindObjectOfType<PlayerMovement>();
        e = FindObjectOfType<Enemy>();
    }
    private void Update()
    {
        if (GM.gameStatus == GameStatus.gameStart)      //se il gioco non � ancora cominciato
        {
            StartCanvas.SetActive(true);                //il men� di inizio si attiva
        }

        if (GM.gameStatus == GameStatus.gameRunning)    //se il gioco � iniziato
        {
            enemiesDefeated.text = GM.numOfDefeated + "/10";                            //il numero di nemici sconfitti si aggiorna in runtime su dieci totali
            if (GM.shield == true)                                                      //e se lo scudo � attivo
            {
                ShieldTimer.fillAmount -= 1 / (PM.shieldTimer + 2) * Time.deltaTime;    //il suo timer (UI) si svuota nel tempo (ho dovuto aggiungere una costante al timer perch� finiva leggermente prima della fine del bonus
            }
            if (GM.malus == true)                                                       //se il malus � attivo
            {
                MalusTimer.fillAmount -= 1 / (e.malusTime + 3) * Time.deltaTime;        //il suo timer (UI) si svuota nel tempo (ho dovuto aggiungere una costante al timer perch� finiva leggermente prima della fine del malus
            }
            if (GM.freeze == true)                                                      //se il freeze � attivo
            {
                FreezeTimer.fillAmount -= 1 / (e.freezingTime + 2) * Time.deltaTime;    //il suo timer (UI) si svuota nel tempo (ho dovuto aggiungere una costante al timer perch� finiva leggermente prima della fine del bonus
            }
            if (GM.ray == false)                                                        //se il raggio non � ancora aumentato
            {
                foreach (GameObject ray in Rays)                                        //ogni raggio (UI) contenuto nell'array sar� inattivo o disattivato
                {
                    ray.SetActive(false);
                }
            }
            else if (GM.ray == true)                                                    //se il raggio � aumentato
            {
                foreach (GameObject ray in Rays)                                        //ogni raggio (UI) contenuto nell'array sar� attivo o attivato
                {
                    ray.SetActive(true);
                }
            }
        }
        if (GM.gameStatus == GameStatus.gameEnd)                //se il gioco � finito e il player ha sconfitto tutti i nemici si attiva il men� di vittoria
        {
            EndCanvas.SetActive(true);
        }
        if (GM.gameStatus == GameStatus.gameDefeat)             //se il gioco � finito e il player � morto si attiva il men� di sconfitta
        {
            DefeatCanvas.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && GM.gameStatus == GameStatus.gameRunning)    //se clicco il tasto ESC e il gioco � in running si attiva il men� di pausa
        {
            GM.gameStatus = GameStatus.gamePaused;
            PauseCanvas.SetActive(true);
        }
        else if (GM.gameStatus == GameStatus.gamePaused && Input.GetKeyDown(KeyCode.Escape))    //se sono in pausa e clicco il tasto ESC
        {
            PLAY();                                                                             //richiamo la funzione che mi permette di tornare in running
        }
    }

    public void PLAY()      //tasto play del men� di start e tasto continue del men� di pausa che rimette il gioco in running
    {
        StartCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        GM.gameStatus = GameStatus.gameRunning;
    }

    public void EXIT()      //tasto exit di tutti i men� che chiude l'applicazione
    {
        Application.Quit();
    }

    public void RESTART()  //tasto restart, retry e main men� del men� di pausa e dei men� di fine che reloadano la scena attualmente attiva
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
