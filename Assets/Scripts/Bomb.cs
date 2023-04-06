using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bomb : MonoBehaviour
{
    AudioManager AM;
    public float timer;
    [SerializeField] GameObject Explosion;
    private const string Dest = "Destructible";
    private const string P = "PowerUp";
    private const string E = "Enemy";
    GameManager GM;
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        AM = FindObjectOfType<AudioManager>();
            AM.explosion.PlayDelayed(2.1f);
        if (GM.ray == false)
        {
            AM.explosion.pitch = 1.4f;
            AM.explosion.volume = 0.3f;
        }

        else
        {
            AM.explosion.pitch = 0.2f;
            AM.explosion.volume = 1;
        }
    }
    void Update()
    {
        if (GM.gameStatus == GameStatus.gameRunning)
        {
            if (GM.ray == true)
            {
                GM.weaponRange = 3;
                Explosion.transform.localScale = new Vector2(20, 20);
            }
            else
            {
                GM.weaponRange = 1.2f;
            }

            timer += Time.deltaTime;
            if (timer >= 2.2f && timer <= 2.5f)
                Explosion.SetActive(true);
            else
                Explosion.SetActive(false);

            if (timer >= 2.5f && timer <= 2.8f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.right, out hit, GM.weaponRange))
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

                if (Physics.Raycast(transform.position, -transform.right, out hit, GM.weaponRange))
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

                if (Physics.Raycast(transform.position, transform.up, out hit, GM.weaponRange))
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

                if (Physics.Raycast(transform.position, -transform.up, out hit, GM.weaponRange))
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
