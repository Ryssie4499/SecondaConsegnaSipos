using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject StartCanvas;      //menù di inizio con play ed exit
    public GameObject EndCanvas;        //menù di vittoria con la possibilità di ricominciare o uscire
    public GameObject DefeatCanvas;     //menù di sconfitta con la possibilità di ritentare o uscire
    public GameObject PauseCanvas;      //menù di pausa con la possibilità di riprendere il gioco, ricominciarlo o uscire

    [Header("In Game UI")]
    public GameObject[] Rays;           //i raggi visibili in alto a sinistra che si illuminano quando il MaxRay è attivo
    public Image ShieldTimer;           //il timer dello scudo
    public Image FreezeTimer;           //il timer del freeze
    public Image MalusTimer;            //il timer del malus
    public Text enemiesDefeated;        //il contatore di nemici sconfitti che si aggiorna quando una bomba colpisce un nemico

    //References
    GameManager GM;                     //ricerco il numero di nemici sconfitti e lo status di gioco
    Enemy e;                            //richiamerò i timer del malus e del freeze
    PlayerMovement PM;                  //richiamerò il timer dello scudo

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        PM = FindObjectOfType<PlayerMovement>();
        e = FindObjectOfType<Enemy>();
    }
    private void Update()
    {
        if (GM.gameStatus == GameStatus.gameStart)      //se il gioco non è ancora cominciato
        {
            StartCanvas.SetActive(true);                //il menù di inizio si attiva
        }

        if (GM.gameStatus == GameStatus.gameRunning)    //se il gioco è iniziato
        {
            enemiesDefeated.text = GM.numOfDefeated + "/10";                            //il numero di nemici sconfitti si aggiorna in runtime su dieci totali
            if (GM.shield == true)                                                      //e se lo scudo è attivo
            {
                ShieldTimer.fillAmount -= 1 / (PM.shieldTimer + 2) * Time.deltaTime;    //il suo timer (UI) si svuota nel tempo (ho dovuto aggiungere una costante al timer perchè finiva leggermente prima della fine del bonus
            }
            if (GM.malus == true)                                                       //se il malus è attivo
            {
                MalusTimer.fillAmount -= 1 / (e.malusTime + 3) * Time.deltaTime;        //il suo timer (UI) si svuota nel tempo (ho dovuto aggiungere una costante al timer perchè finiva leggermente prima della fine del malus
            }
            if (GM.freeze == true)                                                      //se il freeze è attivo
            {
                FreezeTimer.fillAmount -= 1 / (e.freezingTime + 2) * Time.deltaTime;    //il suo timer (UI) si svuota nel tempo (ho dovuto aggiungere una costante al timer perchè finiva leggermente prima della fine del bonus
            }
            if (GM.ray == false)                                                        //se il raggio non è ancora aumentato
            {
                foreach (GameObject ray in Rays)                                        //ogni raggio (UI) contenuto nell'array sarà inattivo o disattivato
                {
                    ray.SetActive(false);
                }
            }
            else if (GM.ray == true)                                                    //se il raggio è aumentato
            {
                foreach (GameObject ray in Rays)                                        //ogni raggio (UI) contenuto nell'array sarà attivo o attivato
                {
                    ray.SetActive(true);
                }
            }
        }
        if (GM.gameStatus == GameStatus.gameEnd)                //se il gioco è finito e il player ha sconfitto tutti i nemici si attiva il menù di vittoria
        {
            EndCanvas.SetActive(true);
        }
        if (GM.gameStatus == GameStatus.gameDefeat)             //se il gioco è finito e il player è morto si attiva il menù di sconfitta
        {
            DefeatCanvas.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && GM.gameStatus == GameStatus.gameRunning)    //se clicco il tasto ESC e il gioco è in running si attiva il menù di pausa
        {
            GM.gameStatus = GameStatus.gamePaused;
            PauseCanvas.SetActive(true);
        }
        else if (GM.gameStatus == GameStatus.gamePaused && Input.GetKeyDown(KeyCode.Escape))    //se sono in pausa e clicco il tasto ESC
        {
            PLAY();                                                                             //richiamo la funzione che mi permette di tornare in running
        }
    }

    public void PLAY()      //tasto play del menù di start e tasto continue del menù di pausa che rimette il gioco in running
    {
        StartCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        GM.gameStatus = GameStatus.gameRunning;
    }

    public void EXIT()      //tasto exit di tutti i menù che chiude l'applicazione
    {
        Application.Quit();
    }

    public void RESTART()  //tasto restart, retry e main menù del menù di pausa e dei menù di fine che reloadano la scena attualmente attiva
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
