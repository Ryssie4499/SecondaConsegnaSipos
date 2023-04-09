using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    public int direction;
    public float range;

    [Header("PowerUp")]
    public float freezingTime;
    public float malusTime;

    //References
    Rigidbody rb;
    GameManager GM;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GM = FindObjectOfType<GameManager>();

        if (GM.freeze == false && GM.gameStatus == GameStatus.gameRunning)
            direction = Random.Range(0, 4);
    }

    void Update()
    {
        if (GM.gameStatus == GameStatus.gameRunning)
        {
            if (freezingTime <= 0)
            {
                GM.freeze = false;
                freezingTime = 5f;
            }
            if (GM.freeze == true && freezingTime >= 0)
            {
                rb.velocity = Vector3.zero;
                freezingTime -= Time.deltaTime;
            }
            else
            {
                switch (direction)
                {
                    case 0:
                        if (GM.malus == false)
                        {
                            rb.velocity = Vector3.up * speed;
                            malusTime = 6f;
                        }
                        else
                        {
                            rb.velocity = Vector3.up * 4 * speed;
                            malusTime -= Time.deltaTime;
                        }
                        break;
                    case 1:
                        if (GM.malus == false)
                        {
                            rb.velocity = Vector3.down * speed;
                            malusTime = 6f;
                        }
                        else
                        {
                            rb.velocity = Vector3.down * 4 * speed;
                            malusTime -= Time.deltaTime;
                        }
                        break;
                    case 2:
                        if (GM.malus == false)
                        {
                            rb.velocity = Vector3.right * speed;
                            malusTime = 6f;
                        }
                        else
                        {
                            rb.velocity = Vector3.right * 4 * speed;
                            malusTime -= Time.deltaTime;
                        }
                        break;
                    case 3:
                        if (GM.malus == false)
                        {
                            rb.velocity = Vector3.left * speed;
                            malusTime = 6f;
                        }
                        else
                        {
                            rb.velocity = Vector3.left * 4 * speed;
                            malusTime -= Time.deltaTime;
                        }
                        break;
                }
                if (malusTime <= 0)
                {
                    GM.malus = false;
                }
                RaycastHit hit;
                if (Physics.Raycast(transform.position, rb.velocity, out hit, range) && hit.transform.name != "Player")
                {
                    direction = Random.Range(0, 4);
                }
            }
        }
        else
            rb.velocity = Vector3.zero * 0f;
    }
}
