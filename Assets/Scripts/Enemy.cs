using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;       //da inspector e da altre classi posso gestire la velocit� dell'enemy
    public int direction;               //la direzione � gestita da uno switch e da una serie di vettori standard (up, down, right, left e zero)
    public float range;                 //il range � la distanza dalla quale l'enemy capta la presenza di un ostacolo e decide in che direzione andare

    [Header("PowerUp")]
    public float freezingTime;          //� il timer relativo al powerUp chiamato freeze, che azzera il vettore di movimento
    public float malusTime;             //questo invece � il timer del boost del nemico

    //References
    Rigidbody rb;                       //il rigidbody servir� per dare all'enemy un vettore di movimento
    GameManager GM;                     //dal gameManager recupero lo status di gioco e le booleane relative all'attivazione dei powerUp
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GM = FindObjectOfType<GameManager>();

        //se il powerUp Freeze non � attivo e il gioco � in running la direzione di movimento � un numero randomico tra 0 compreso e 4 escluso
        if (GM.freeze == false && GM.gameStatus == GameStatus.gameRunning)
        {
            direction = Random.Range(0, 4);
        }
    }

    void Update()
    {
        if (GM.gameStatus == GameStatus.gameRunning)        //se lo status di gioco � in running...
        {
            if (freezingTime <= 0)                          //e se il timer � giunto al termine del suo countdown (o � settato a zero di default)
            {
                GM.freeze = false;                          //il powerUp freeze viene disattivato (o � inattivo fino a quando non viene raccolto il powerUp)
                freezingTime = 5f;                          //il timer viene settato al suo massimo
            }
            if (GM.freeze == true && freezingTime >= 0)     //e quando il freeze � attivo, con timer maggiore di zero
            {
                rb.velocity = Vector3.zero;                 //il vettore di movimento dell'enemy � nullo
                freezingTime -= Time.deltaTime;             //e inizia il countdown
            }

            //se invece il freeze � inattivo
            else
            {
                switch (direction)                                  //la direzione di movimento viene decisa in base al numero che viene scelto randomicamente allo Start
                {
                    case 0:
                        if (GM.malus == false)                      //nel caso zero in cui il boost non sia attivo
                        {
                            rb.velocity = Vector3.up * speed;       //il vettore di movimento � verso l'alto con la velocit� settata da inspector
                            malusTime = 6f;                         //e il timer del boost viene settato al massimo per poter poi scendere quando verr� raccolto il malus
                        }
                        else
                        {
                            rb.velocity = Vector3.up * 4 * speed;   //se invece il malus(boost) � attivo, il vettore verso l'alto e la velocit� di movimento, vengono quadruplicati
                            malusTime -= Time.deltaTime;            //e il timer del boost comincia il countdown
                        }
                        break;
                    case 1:
                        if (GM.malus == false)                      //nel caso uno in cui il boost non sia attivo
                        {
                            rb.velocity = Vector3.down * speed;     //il vettore di movimento � verso il basso con la velocit� settata da inspector
                            malusTime = 6f;                         //e il timer del boost viene settato al massimo per poter poi scendere quando verr� raccolto il malus
                        }
                        else
                        {
                            rb.velocity = Vector3.down * 4 * speed; //se invece il malus(boost) � attivo, il vettore verso il basso e la velocit� di movimento, vengono quadruplicati
                            malusTime -= Time.deltaTime;            //e il timer del boost comincia il countdown
                        }
                        break;
                    case 2:
                        if (GM.malus == false)                      //nel caso due in cui il boost non sia attivo
                        {
                            rb.velocity = Vector3.right * speed;    //il vettore di movimento � verso destra con la velocit� settata da inspector
                            malusTime = 6f;                         //e il timer del boost viene settato al massimo per poter poi scendere quando verr� raccolto il malus
                        }
                        else
                        {
                            rb.velocity = Vector3.right * 4 * speed;//se invece il malus(boost) � attivo, il vettore verso destra e la velocit� di movimento, vengono quadruplicati
                            malusTime -= Time.deltaTime;            //e il timer del boost comincia il countdown
                        }
                        break;
                    case 3:
                        if (GM.malus == false)                      //nel caso tre in cui il boost non sia attivo
                        {
                            rb.velocity = Vector3.left * speed;     //il vettore di movimento � verso sinistra con la velocit� settata da inspector
                            malusTime = 6f;                         //e il timer del boost viene settato al massimo per poter poi scendere quando verr� raccolto il malus
                        }
                        else
                        {
                            rb.velocity = Vector3.left * 4 * speed; //se invece il malus(boost) � attivo, il vettore verso sinistra e la velocit� di movimento, vengono quadruplicati
                            malusTime -= Time.deltaTime;            //e il timer del boost comincia il countdown
                        }
                        break;
                }
                if (malusTime <= 0)                                 //se il timer del boost scende a zero o sotto zero il malus si disattiva
                {
                    GM.malus = false;
                }

                //strutturo un Raycast che parta dalla posizione dell'enemy, che abbia come fine il vettore di direzione attuale e che colpisca gli oggetti (tranne il player) entro un range predefinito
                RaycastHit hit;
                if (Physics.Raycast(transform.position, rb.velocity, out hit, range) && hit.transform.name != "Player")
                {
                    direction = Random.Range(0, 4);             //quando il raycast colpisce una superficie, fa scegliere randomicamente all'enemy quale direzione prendere
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;         //quando il game status non � in game running, i nemici non si muovono
        }
    }
}
