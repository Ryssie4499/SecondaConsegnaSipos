using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float playerSpeed = 20f;
    [SerializeField] float boundaryHeight = 17f;
    [SerializeField] float boundaryWeight = 32f;
    Rigidbody rb;
    Vector2 pOldPos;
    Vector2 movementDirection;
    //Vector2 curPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //curPos = transform.position;
    }


    void Update()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //if (transform.position.x % 0.5f == 0 && transform.position.y % 0.5f == 0)
        //{
            movementDirection = new Vector2(horizontal, vertical);
            rb.velocity = movementDirection * playerSpeed;
        //}
        //else
        //{
        //    float num1 = Mathf.Round(transform.position.x);
        //    float num2 = num1 + (num1 / 10);
        //    curPos.x = num2;

        //    float n1 = Mathf.Round(transform.position.y);
        //    float n2 = n1 + (n1 / 10);
        //    curPos.y = n2;
        //}

        if (player.transform.position.y > boundaryHeight)
        {
            player.transform.position = new Vector2(pOldPos.x, Mathf.RoundToInt(pOldPos.y));
        }
        if (player.transform.position.y < 1)
        {
            player.transform.position = new Vector2(pOldPos.x, Mathf.RoundToInt(pOldPos.y));
        }
        if (player.transform.position.x > boundaryWeight)
        {
            player.transform.position = new Vector2(pOldPos.x, Mathf.RoundToInt(pOldPos.y));
        }
        if (player.transform.position.x < 1)
        {
            player.transform.position = new Vector2(pOldPos.x, Mathf.RoundToInt(pOldPos.y));
        }
        pOldPos = player.transform.position;
    }
}
