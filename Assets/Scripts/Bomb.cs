using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float timer;
    [SerializeField] GameObject Explosion;
    [SerializeField] float weaponRange = 1;
    private const string Dest = "Destructible";
    private const string P = "PowerUp";
    private const string E = "Enemy";
    MapGenerator mG;
    //[SerializeField] LayerMask mask;
    private void Start()
    {
        mG = FindObjectOfType<MapGenerator>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2.2f && timer <= 2.5f)
            Explosion.SetActive(true);
        else
            Explosion.SetActive(false);
        if(timer>=2.5f && timer<=2.8f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.right, out hit, weaponRange))
            {
                if(hit.transform.name == E||hit.transform.name == Dest || hit.transform.name == P)
                {
                    Destroy(hit.transform.gameObject);
                }
                else if(hit.transform.CompareTag("Player"))
                {
                    Debug.Log("You Died!");
                }
                Debug.DrawRay(transform.position, transform.right * weaponRange, Color.red, 1);
            }

            if (Physics.Raycast(transform.position, -transform.right, out hit, weaponRange))
            {
                if(hit.transform.name == E||hit.transform.name == Dest || hit.transform.name == P)
                {
                    Destroy(hit.transform.gameObject);
                }
                else if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("You Died!");
                }
                Debug.DrawRay(transform.position, -transform.right * weaponRange, Color.red, 1);
            }

            if (Physics.Raycast(transform.position, transform.up, out hit, weaponRange))
            {
                if (hit.transform.name == E || hit.transform.name == Dest|| hit.transform.name == P)
                {
                    Destroy(hit.transform.gameObject);
                }
                else if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("You Died!");
                }
                Debug.DrawRay(transform.position, transform.up * weaponRange, Color.red, 1);
            }

            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponRange))
            {
                if (hit.transform.name == E || hit.transform.name == Dest || hit.transform.name == P)
                {
                    Destroy(hit.transform.gameObject);
                }
                else if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("You Died!");
                }
                Debug.DrawRay(transform.position, -transform.up * weaponRange, Color.red, 1);
            }

        }
        else if(timer>=3f)
        {
            Destroy(gameObject);
            timer = 0;
        }
    }
}
