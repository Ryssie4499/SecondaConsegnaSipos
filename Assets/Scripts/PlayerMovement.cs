using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float playerSpeed = 20f;
    [SerializeField] float boundaryHeight = 32f;
    [SerializeField] float boundaryWeight = 32f;
    Rigidbody rb;
    Vector2 pOldPos;
    Vector2 movementDirection;

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
        Mathf.RoundToInt(gameObject.transform.position.x);
        Mathf.RoundToInt(gameObject.transform.position.y);
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector2(horizontal, vertical);
        rb.velocity = movementDirection * playerSpeed;

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


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CameraMove"))
        {
            if (player.transform.position.x >= 18)
            {
                Camera.main.transform.position = new Vector3(21f, Camera.main.transform.position.y, Camera.main.transform.position.z);
            }
            if (player.transform.position.x <= 16)
            {
                Camera.main.transform.position = new Vector3(8.5f, Camera.main.transform.position.y, Camera.main.transform.position.z);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("You Died!");
        }
    }
}
