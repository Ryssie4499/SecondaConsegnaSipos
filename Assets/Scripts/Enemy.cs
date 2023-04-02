using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    public int direction;
    public float range;
    public float freezingTime = 10f;
    Rigidbody rb;
    GameManager GM;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GM = FindObjectOfType<GameManager>();
        if (GM.freeze == false)
            direction = Random.Range(0, 4);
    }

    void Update()
    {


        if (GM.freeze == true)
        {
            rb.velocity = Vector3.zero * 0f;
            StartCoroutine(TimeOfFreeze());
        }
        else
        {

            switch (direction)
            {
                case 0:
                    if (GM.malus == false)
                        rb.velocity = Vector3.up * speed;
                    else
                    {
                        rb.velocity = Vector3.up * 4 * speed;
                        StartCoroutine(TimeMalus());
                    }
                    break;
                case 1:
                    if (GM.malus == false)
                        rb.velocity = Vector3.down * speed;
                    else
                    {
                        rb.velocity = Vector3.down * 4 * speed;
                        StartCoroutine(TimeMalus());
                    }
                    break;
                case 2:
                    if (GM.malus == false)
                        rb.velocity = Vector3.right * speed;
                    else
                    {
                        rb.velocity = Vector3.right * 4 * speed;
                        StartCoroutine(TimeMalus());
                    }
                    break;
                case 3:
                    if (GM.malus == false)
                        rb.velocity = Vector3.left * speed;
                    else
                    {
                        rb.velocity = Vector3.left * 4 * speed;
                        StartCoroutine(TimeMalus());
                    }
                    break;
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rb.velocity, out hit, range) && hit.transform.name != "Player")
            {
                direction = Random.Range(0, 4);
            }
        }

    }
    IEnumerator TimeOfFreeze()
    {
        yield return new WaitForSeconds(freezingTime);
        GM.freeze = false;
    }
    IEnumerator TimeMalus()
    {
        yield return new WaitForSeconds(6);
        GM.malus = false;
    }

}
