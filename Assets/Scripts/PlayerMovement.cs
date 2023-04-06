using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float shieldTimer;
    public float speed = 5f;
    public float cameraSpeed = 0.01f;
    private Vector2 direction;
    public Rigidbody rb;
    public Collider c;
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

                if (GM.player.position.y > GM.boundaryHeight || GM.player.position.y <= 1)
                {
                    GM.player.position = new Vector2(GM.pOldPos.x, Mathf.RoundToInt(GM.pOldPos.y));
                }
                if (GM.player.position.x > GM.boundaryWidth || GM.player.position.x <= 1)
                {
                    GM.player.position = new Vector2(Mathf.RoundToInt(GM.pOldPos.x), GM.pOldPos.y);
                }
                GM.pOldPos = GM.player.position;
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
