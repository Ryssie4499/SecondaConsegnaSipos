using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float playerSpeed = 100f;
    [SerializeField] float boundaryHeight = 17f;
    [SerializeField] float boundaryWeight = 32f;
    Rigidbody rb;
    Vector2 pOldPos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        float yMove = Input.GetAxis("Vertical");
        float xMove = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(xMove, yMove, rb.velocity.y) * playerSpeed;

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
