using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource explosion;               //in inspector sar� possibile inserire la traccia audio dell'esplosione

    [Header("Prefab")]
    [SerializeField] GameObject Explosion;      //questo � il prefab del raggio di esplosione da attivare solo quando la bomba � pronta ad esplodere

    float timer;                                //il timer � un countdown prima dell'esplosione e del controllo dei raycast
    float weaponRange = 1.1f;                   //il range � leggermente pi� grande della singola unit� per rendere pi� semplice al player colpire l'enemy se dovesse essere in movimento

    private const string Dest = "Destructible"; //questa stringa verr� utilizzata per richiamare il nome del blocco colpito con il raycast
    private const string E = "Enemy";           //questa stringa invece verr�a utilizzata per richiamare il tag dell'enemy colpito dal raycast

    //References
    GameManager GM;                             //dal gameManager richiamo gli status di gioco e la booleana del powerUp MaxRay
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();

        if (GM.ray == false)                    //se il raggio aumentato � inattivo il pitch della sua traccia acustica � pi� alto e il volume � pi� basso
        {
            explosion.pitch = 1.4f;
            explosion.volume = 0.2f;
        }

        else                                   //se il raggio aumentato � attivo il pitch della sua traccia acustica � pi� basso e il volume � pi� alto
        {
            explosion.pitch = 0.2f;
            explosion.volume = 1;
        }
    }
    void Update()
    {
        if (GM.gameStatus == GameStatus.gameRunning)            //solo in game running la bomba pu� fare il controllo del raggio e del raycast
        {
            if (GM.ray == true)                                 //se il MaxRay � attivo, il range � pi� grande e anche la sprite che raffigura l'esplosione
            {
                weaponRange = 3;
                Explosion.transform.localScale = new Vector2(20, 20);
            }
            else
            {
                weaponRange = 1.1f;                             //se invece il MaxRay non � attivo, il range torna a 1.1
            }

            timer += Time.deltaTime;                            //il timer dell'esplosivo comincia il countdown dal momento in cui viene istanziata la bomba
            if (timer >= 2.2f && timer <= 2.5f)                 //quando il timer si trova tra quei due valori, viene attivata la sprite che raffigura l'esplosione
            {
                Explosion.SetActive(true);
            }
            else
            {
                Explosion.SetActive(false);                     //in ogni altro momento, rimane/viene disattivata
            }
        
            if(timer >= 2.2f && timer <= 2.22f)                 //ritardo l'inizio della traccia acustica dell'esplosione a quando effettivamente esplode la bomba
            {
                explosion.Play();
            }

            if (timer >= 2.5f && timer <= 2.8f)                 //se il timer ha gi� superato l'esplosione, ha meno di un secondo per distruggere i blocchi o i nemici
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.right, out hit, weaponRange))     //raycast che punta a destra del nemico entro il range di 1.1
                {
                    if (hit.transform.tag == E || hit.transform.name == Dest)                       //se colpisce un nemico o un blocco distruttibile, lo distrugge e se il raggio aumentato era attivo, ora � disattivato
                    {
                        if (hit.transform.tag == E)                                                 //in particolare se il tag dell'oggetto colpito � quello dell'enemy, il numero di nemici sconfitti aumenta
                        {
                            GM.numOfDefeated++;
                        }
                        Destroy(hit.transform.gameObject);
                        GM.ray = false;
                    }
                    else if (hit.transform.CompareTag("Player"))                                    //se si tratta invece del player, lo status di gioco passa a quello di sconfitta
                    {
                        GM.gameStatus = GameStatus.gameDefeat;
                    }
                }

                if (Physics.Raycast(transform.position, -transform.right, out hit, weaponRange))    //raycast che punta a sinistra del nemico entro il range di 1.1
                {
                    if (hit.transform.tag == E || hit.transform.name == Dest)                       //se colpisce un nemico o un blocco distruttibile, lo distrugge e se il raggio aumentato era attivo, ora � disattivato
                    {
                        if (hit.transform.tag == E)                                                 //in particolare se il tag dell'oggetto colpito � quello dell'enemy, il numero di nemici sconfitti aumenta
                        {
                            GM.numOfDefeated++;
                        }
                        Destroy(hit.transform.gameObject);
                        GM.ray = false;
                    }
                    else if (hit.transform.CompareTag("Player"))                                    //se si tratta invece del player, lo status di gioco passa a quello di sconfitta
                    {
                        GM.gameStatus = GameStatus.gameDefeat;
                    }
                }

                if (Physics.Raycast(transform.position, transform.up, out hit, weaponRange))        //raycast che punta sopra al nemico entro il range di 1.1
                {
                    if (hit.transform.tag == E || hit.transform.name == Dest)                       //se colpisce un nemico o un blocco distruttibile, lo distrugge e se il raggio aumentato era attivo, ora � disattivato
                    {
                        if (hit.transform.tag == E)                                                 //in particolare se il tag dell'oggetto colpito � quello dell'enemy, il numero di nemici sconfitti aumenta
                        {
                            GM.numOfDefeated++;
                        }
                        Destroy(hit.transform.gameObject);
                        GM.ray = false;
                    }
                    else if (hit.transform.CompareTag("Player"))                                    //se si tratta invece del player, lo status di gioco passa a quello di sconfitta
                    {
                        GM.gameStatus = GameStatus.gameDefeat;
                    }
                }

                if (Physics.Raycast(transform.position, -transform.up, out hit, weaponRange))        //raycast che punta sotto al nemico entro il range di 1.1
                {
                    if (hit.transform.tag == E || hit.transform.name == Dest)                       //se colpisce un nemico o un blocco distruttibile, lo distrugge e se il raggio aumentato era attivo, ora � disattivato
                    {
                        if (hit.transform.tag == E)                                                 //in particolare se il tag dell'oggetto colpito � quello dell'enemy, il numero di nemici sconfitti aumenta
                        {
                            GM.numOfDefeated++;
                        }
                        Destroy(hit.transform.gameObject);
                        GM.ray = false;
                    }
                    else if (hit.transform.CompareTag("Player"))                                    //se si tratta invece del player, lo status di gioco passa a quello di sconfitta
                    {
                        GM.gameStatus = GameStatus.gameDefeat;
                    }
                }
            }
            else if (timer >= 3f)           //nel caso in cui il timer abbia superato anche il controllo dei raycast, la bomba si autodistrugge e il timer si resetta
            {
                Destroy(gameObject);
                timer = 0;
            }
        }
    }
}
