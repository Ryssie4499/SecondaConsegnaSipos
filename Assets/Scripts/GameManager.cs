using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creo un enum con vari status di gioco
public enum GameStatus
{
    gamePaused,     //il gioco � in pausa: verr� fuori un men� con la possibilit� di riprendere il gioco, ricominciare la partita o uscire. Si attiva quando viene premuto il tasto ESC e pu� essere chiuso con lo stesso tasto.
    gameRunning,    //il gioco � in funzione: ogni meccanica, dal movimento dei nemici, all'esplosione delle bombe e al funzionamento dei power up, saranno attivi solo in questa modalit�.
    gameEnd,        //hai vinto: verr� fuori un men� di fine, con la possibilit� di uscire o ricominciare una nuova partita. Si attiva quando viene ucciso l'ultimo nemico.
    gameStart,      //il gioco ha ancora da cominciare: ogni meccanica � in attesa di passare in game running per funzionare. Il men� permette di cominciare la partita o uscire.
    gameDefeat      //hai perso: viene fuori solo se vieni colpito da un nemico o dall'esplosione di una bomba da te piazzata. Il men� permette di ricominciare la partita da capo o uscire.
}
public class GameManager : MonoBehaviour
{
    //creo delle booleane che segnalino se il power up � attivo o meno e le dichiaro nel GameManager per non dover creare interdipendenze fra pi� codici visto che vengono richiamate sia in PowerUp, che in Bomb e UIManager
    [HideInInspector] public bool ray;      //ray (o MaxRay) � il powerUp che aumenta il raggio di esplosione delle bombe per un unico tentativo andato a buon fine (se non colpisci niente, puoi riprovare).
    [HideInInspector] public bool shield;   //shield � il powerUp che ti permette di attraversare ogni parete e nemico senza subire danno. L'unico tipo di blocco non superabile � il boundary.
    [HideInInspector] public bool freeze;   //freeze blocca temporaneamente i nemici, permettendoti di raggiungere un powerUp, scappare o piazzare una bomba vicino al nemico.
    [HideInInspector] public bool malus;    //l'unico malus del gioco � un boost per i nemici. La loro velocit� aumenta temporaneamente.
    [HideInInspector] public int shieldNum; //il numero di shield raccolti
    [HideInInspector] public int freezeNum; //il numero di freeze raccolti
    [HideInInspector] public int malusNum;  //il numero di malus raccolti
    [HideInInspector] public int maxRayNum; //il numero di maxRay raccolti

    [Header("Status")]
    public GameStatus gameStatus;   //da Inspector e dalle altre classi sar� possibile modificare manualmente lo status di gioco in determinate situazioni.

    [Header("Enemies")]
    public float numOfDefeated;     //il numero di nemici sconfitti aumenta mano a mano che ne uccidi e si resetta quando perdi.
    
    private void Update()
    {
        if(numOfDefeated == 10f && gameStatus == GameStatus.gameRunning)    //se il numero di nemici sconfitti � uguale a 10 (il numero di nemici presenti sulla mappa) e lo status di gioco � in running...
        {
            gameStatus = GameStatus.gameEnd;                                //la partita finisce e appare la schermata di vittoria che permette di ricominciare la partita da capo o uscire.
        }
    }
}
