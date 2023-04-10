using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creo un enum dei tipi di powerUp disponibili sulla mappa
public enum TypeOfPowerUp
{
    MaxRay,         //se si tratta di un MaxRay, appena raccolto, aumenter� il raggio della bomba appena piazzata o ancora da piazzare finch� non colpirai un blocco distruttibile o un nemico
    Shield,         //se raccogli uno shield, puoi attraversare qualunque parete tranne i contorni della mappa e attraversare i nemici senza essere ucciso, ma non sar� possibile raccogliere i powerUp ancora nascosti
    EnemyFreeze,    //il freeze blocca i nemici per qualche secondo, permettendoti di avvicinarti a loro o di scappare prima che ti uccidano
    EnemyBoost      //� l'unico tipo di malus presente in mappa e aumenta la velocit� dei nemici per qualche secondo
}
public class PowerUp : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource powerUpAudio;    //sar� possibile da inspector inserire l'audio pi� appropriato al tipo di powerUp

    [Header("PowerUp Type")]
    public TypeOfPowerUp type;          //sia da inspector che da altre classi � possibile impostare il tipo di powerUp in questione

    //References
    GameManager GM;                     //dal gameManager necessito di poter accedere allo status di gioco, per gestire il conteggio dei powerUp e alle booleane che segnalano l'attivit� di un powerUp
    UIManager UM;                       //lo UIManager mi serve per gestire il fill momentaneo dei timer dei powerUp e per aggiornare il conteggio dei powerUp

    //recupero gli script per poter richiamare le loro variabili
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        UM = FindObjectOfType<UIManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GM.gameStatus == GameStatus.gameRunning && other.CompareTag("Player") && GM.shield == false)    //quando il player entra nel trigger dei powerUp, lo shield non � ancora attivo e lo status di gioco � in running...
        {
            if (type == TypeOfPowerUp.MaxRay)       //se il tipo di powerUp raccolto � un MaxRay...
            {
                GM.maxRayNum++;
                powerUpAudio.Play();                //attivo la traccia audio assegnata da inspector
                GM.ray = true;                      //la booleana diventa true
                Destroy(gameObject);                //e il powerUp si auto-distrugge
            }
            else if (type == TypeOfPowerUp.Shield)  //se il tipo di powerUp raccolto � uno scudo e prima non ne possedevo uno...
            {
                GM.shieldNum++;
                powerUpAudio.Play();                //attivo la traccia audio assegnata da inspector
                UM.ShieldTimer.fillAmount = 1;      //il fill del timer si setta al massimo prima di scendere
                GM.shield = true;                   //la booleana diventa true
                Destroy(gameObject);                //e il powerUp si auto-distrugge
            }
            else if (type == TypeOfPowerUp.EnemyFreeze)
            {
                GM.freezeNum++;
                powerUpAudio.Play();                //attivo la traccia audio assegnata da inspector
                UM.FreezeTimer.fillAmount = 1;      //il fill del timer si setta al massimo prima di scendere
                GM.freeze = true;                   //la booleana diventa true
                Destroy(gameObject);                //e il powerUp si auto-distrugge
            }
            else if (type == TypeOfPowerUp.EnemyBoost)
            {
                GM.malusNum++;
                powerUpAudio.Play();                //attivo la traccia audio assegnata da inspector
                UM.MalusTimer.fillAmount = 1;       //il fill del timer si setta al massimo prima di scendere
                GM.malus = true;                    //la booleana diventa true
                Destroy(gameObject);                //e il powerUp si auto-distrugge
            }
        }
        //se lo shield � gi� attivo, anche entrando nel trigger degli altri powerUp, non li si raccoglierebbe e non si otterrebbero i loro benefici
    }
}
