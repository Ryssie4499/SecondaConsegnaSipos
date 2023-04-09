using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Camera")]
    public float cameraSpeed = 0.01f;               //do una velocità di movimento alla camera, così quando ci sarà lo spostamento dall'alto al basso e viceversa, lo farà lentamente

    [Header("Player")]
    public float shieldTimer;                       //timer dello scudo
    public float speed = 5f;                        //velocità di movimento del player
    private Vector2 direction;                      //vettore di movimento del player
    [HideInInspector] public Vector2 pOldPos;       //vecchia posizione del player utilizzata per evitare che il player superi i boundaries

    [Header("Map")]
    public float boundaryHeight;                    //altezza della mappa
    public float boundaryWidth;                     //larghezza della mappa

    //References
    [HideInInspector] public Rigidbody rb;          //rigidbody del player per dargli un vettore di movimento
    [HideInInspector] public Collider c;            //collider da attivare e disattivare con lo scudo
    GameManager GM;                                 //richiamo lo status di gioco e la booleana dell'attivazione dello scudo dal GameManager


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        c = GetComponent<Collider>();
        GM = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (GM.gameStatus == GameStatus.gameRunning)                //se il gioco è in running
        {
            if (GM.shield == true && shieldTimer >= 0)              //e lo scudo è attivo con timer maggiore di zero
            {
                shieldTimer -= Time.deltaTime;                      //il countdown ha inizio
                c.isTrigger = true;                                 //e il collider diventa trigger

                //se lo scudo è attivo bisogna controllare la posizione del player e se supera i boundaries, bisogna farlo tornare alla posizione precedente
                if (transform.position.y > boundaryHeight || transform.position.y <= 1) 
                {
                    transform.position = new Vector2(pOldPos.x, Mathf.RoundToInt(pOldPos.y));
                }
                if (transform.position.x > boundaryWidth || transform.position.x <= 1)
                {
                    transform.position = new Vector2(Mathf.RoundToInt(pOldPos.x), pOldPos.y);
                }
                pOldPos = transform.position;
            }
            else if (GM.shield == false)    //se lo scudo non è attivo
            {
                c.isTrigger = false;        //il collider non è più trigger
                shieldTimer = 5f;           //e il timer torna al massimo
            }

            if (shieldTimer <= 0)           //se il countdown giunge al termine lo scudo si disattiva
            {
                GM.shield = false;
            }

            //se il player si trova sopra o sotto una certa posizione, la camera slitta verso l'alto o verso il basso in determinate posizioni
            if (transform.position.y >= 17)
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, 22f, Camera.main.transform.position.z), cameraSpeed);
            else if (transform.position.y <= 15)
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, 9f, Camera.main.transform.position.z), cameraSpeed);

            //se clicco i tasti WASD avrò le direzioni corrispettive tradotte in vettori
            if (Input.GetKey(KeyCode.W))
            {
                SetDirection(Vector2.up);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                SetDirection(Vector2.down);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                SetDirection(Vector2.left);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                SetDirection(Vector2.right);
            }
            else
            {
                SetDirection(Vector2.zero);    //se non clicco niente, il movimento ha vettore azzerato e la posizione si arrotonda all'intero più vicino per evitare che il player si incastri tra un blocco e l'altro
                gameObject.transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            }
            Vector2 position = rb.position;
            Vector2 translation = direction * speed * Time.deltaTime;

            rb.MovePosition(position + translation);        //il movimento a determinate posizioni è dato dalla somma tra la posizione del player, la sua direzione, la velocità e il tempo impiegato per percorrere il tratto
        }
    }

    private void SetDirection(Vector2 newDirection)     //assegno una direzione nuova al player ogni volta che cambio tasto
    {
        direction = newDirection;
    }

    private void OnCollisionEnter(Collision coll)       //se entra in collisione con il nemico e il gioco è in running il player perde
    {
        if (coll.collider.CompareTag("Enemy") && GM.gameStatus == GameStatus.gameRunning)
        {
            GM.gameStatus = GameStatus.gameDefeat;
        }
    }

}
