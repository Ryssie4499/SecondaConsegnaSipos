using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    public float timer;
    [SerializeField] GameObject Explosion;
    //[SerializeField] public float weaponRange = 1;
    private const string Dest = "Destructible";
    private const string P = "PowerUp";
    private const string E = "Enemy";
    GameManager GM;
    //[SerializeField] LayerMask mask;
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (GM.ray == true)
        {
            GM.weaponRange = 3;
            Explosion.transform.localScale = new Vector2(20,20);
        }
        else
        {
            GM.weaponRange = 1;
        }

        timer += Time.deltaTime;
        if (timer >= 2.2f && timer <= 2.5f)
            Explosion.SetActive(true);
        else
            Explosion.SetActive(false);

        if(timer>=2.5f && timer<=2.8f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.right, out hit, GM.weaponRange))
            {
                if(hit.transform.name == E||hit.transform.name == Dest || hit.transform.name == P)
                {
                    if(hit.transform.name == E)
                    {
                        GM.numOfDefeated++;
                    }
                    Destroy(hit.transform.gameObject);
                    GM.ray = false;
                }
                else if(hit.transform.CompareTag("Player"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                Debug.DrawRay(transform.position, transform.right * GM.weaponRange, Color.red, 1);
            }

            if (Physics.Raycast(transform.position, -transform.right, out hit, GM.weaponRange))
            {
                if(hit.transform.name == E||hit.transform.name == Dest || hit.transform.name == P)
                {
                    if (hit.transform.name == E)
                    {
                        GM.numOfDefeated++;
                    }
                    Destroy(hit.transform.gameObject);
                    GM.ray = false;
                }
                else if (hit.transform.CompareTag("Player"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                Debug.DrawRay(transform.position, -transform.right * GM.weaponRange, Color.red, 1);
            }

            if (Physics.Raycast(transform.position, transform.up, out hit, GM.weaponRange))
            {
                if (hit.transform.name == E || hit.transform.name == Dest|| hit.transform.name == P)
                {
                    if (hit.transform.name == E)
                    {
                        GM.numOfDefeated++;
                    }
                    Destroy(hit.transform.gameObject);
                    GM.ray = false;
                }
                else if (hit.transform.CompareTag("Player"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                Debug.DrawRay(transform.position, transform.up * GM.weaponRange, Color.red, 1);
            }

            if (Physics.Raycast(transform.position, -transform.up, out hit, GM.weaponRange))
            {
                if (hit.transform.name == E || hit.transform.name == Dest || hit.transform.name == P)
                {
                    if (hit.transform.name == E)
                    {
                        GM.numOfDefeated++;
                    }
                    Destroy(hit.transform.gameObject);
                    GM.ray = false;
                }
                else if (hit.transform.CompareTag("Player"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                Debug.DrawRay(transform.position, -transform.up * GM.weaponRange, Color.red, 1);
            }

        }
        else if(timer>=3f)
        {
            Destroy(gameObject);
            timer = 0;
        }
    }
}
