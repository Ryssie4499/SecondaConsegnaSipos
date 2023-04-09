using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource explosion;

    [Header("Prefab")]
    [SerializeField] GameObject Explosion;

    [HideInInspector] public float timer;
    [HideInInspector] public float weaponRange = 1.2f;

    private const string Dest = "Destructible";
    private const string P = "PowerUp";
    private const string E = "Enemy";

    //References
    GameManager GM;
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();

        explosion.PlayDelayed(2.1f);
        if (GM.ray == false)
        {
            explosion.pitch = 1.4f;
            explosion.volume = 0.2f;
        }

        else
        {
            explosion.pitch = 0.2f;
            explosion.volume = 1;
        }
    }
    void Update()
    {
        if (GM.gameStatus == GameStatus.gameRunning)
        {
            if (GM.ray == true)
            {
                weaponRange = 3;
                Explosion.transform.localScale = new Vector2(20, 20);
            }
            else
            {
                weaponRange = 1.2f;
            }

            timer += Time.deltaTime;
            if (timer >= 2.2f && timer <= 2.5f)
                Explosion.SetActive(true);
            else
                Explosion.SetActive(false);

            if (timer >= 2.5f && timer <= 2.8f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.right, out hit, weaponRange))
                {
                    if (hit.transform.tag == E || hit.transform.name == Dest || hit.transform.name == P)
                    {
                        if (hit.transform.tag == E)
                        {
                            GM.numOfDefeated++;
                        }
                        Destroy(hit.transform.gameObject);
                        GM.ray = false;
                    }
                    else if (hit.transform.CompareTag("Player"))
                    {
                        GM.gameStatus = GameStatus.gameDefeat;
                    }
                }

                if (Physics.Raycast(transform.position, -transform.right, out hit, weaponRange))
                {
                    if (hit.transform.tag == E || hit.transform.name == Dest || hit.transform.name == P)
                    {
                        if (hit.transform.tag == E)
                        {
                            GM.numOfDefeated++;
                        }
                        Destroy(hit.transform.gameObject);
                        GM.ray = false;
                    }
                    else if (hit.transform.CompareTag("Player"))
                    {
                        GM.gameStatus = GameStatus.gameDefeat;
                    }
                }

                if (Physics.Raycast(transform.position, transform.up, out hit, weaponRange))
                {
                    if (hit.transform.tag == E || hit.transform.name == Dest || hit.transform.name == P)
                    {
                        if (hit.transform.tag == E)
                        {
                            GM.numOfDefeated++;
                        }
                        Destroy(hit.transform.gameObject);
                        GM.ray = false;
                    }
                    else if (hit.transform.CompareTag("Player"))
                    {
                        GM.gameStatus = GameStatus.gameDefeat;
                    }
                }

                if (Physics.Raycast(transform.position, -transform.up, out hit, weaponRange))
                {
                    if (hit.transform.tag == E || hit.transform.name == Dest || hit.transform.name == P)
                    {
                        if (hit.transform.tag == E)
                        {
                            GM.numOfDefeated++;
                        }
                        Destroy(hit.transform.gameObject);
                        GM.ray = false;
                    }
                    else if (hit.transform.CompareTag("Player"))
                    {
                        GM.gameStatus = GameStatus.gameDefeat;
                    }
                }
            }
            else if (timer >= 3f)
            {
                Destroy(gameObject);
                timer = 0;
            }
        }
    }
}
