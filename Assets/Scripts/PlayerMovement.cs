using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Camera")]
    public float cameraSpeed = 0.01f;

    [Header("Player")]
    public float shieldTimer;
    public float speed = 5f;
    private Vector2 direction;
    [HideInInspector] public Vector2 pOldPos;

    [Header("Map")]
    public float boundaryHeight;
    public float boundaryWidth;

    //References
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Collider c;
    GameManager GM;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        c = GetComponent<Collider>();
        GM = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (GM.gameStatus == GameStatus.gameRunning)
        {
            if (GM.shield == true && shieldTimer >= 0)
            {
                shieldTimer -= Time.deltaTime;
                c.isTrigger = true;

                if (transform.position.y > boundaryHeight || transform.position.y <= 1)
                {
                    transform.position = new Vector2(pOldPos.x, Mathf.RoundToInt(pOldPos.y));
                }
                if (transform.position.x > boundaryWidth || transform.position.x <= 1)
                {
                    transform.position = new Vector2(Mathf.RoundToInt(pOldPos.x), pOldPos.y);
                }
                pOldPos = transform.position;
            }
            else if (GM.shield == false)
            {
                c.isTrigger = false;
                shieldTimer = 5f;
            }

            if (shieldTimer <= 0)
            {
                GM.shield = false;
            }

            if (transform.position.y >= 17)
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, 22f, Camera.main.transform.position.z), cameraSpeed);
            else if (transform.position.y <= 15)
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, 9f, Camera.main.transform.position.z), cameraSpeed);


            if (Input.GetKey(KeyCode.W))
            {
                SetDirection(Vector2.up);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                SetDirection(Vector2.down);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                SetDirection(Vector2.left);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                SetDirection(Vector2.right);
            }
            else
            {
                SetDirection(Vector2.zero);
                gameObject.transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            }
            Vector2 position = rb.position;
            Vector2 translation = direction * speed * Time.deltaTime;

            rb.MovePosition(position + translation);
        }
    }

    private void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Enemy") && c.isTrigger == false && GM.gameStatus == GameStatus.gameRunning)
        {
            GM.gameStatus = GameStatus.gameDefeat;
        }
    }

}
