using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    private Vector2 direction;
    public float speed = 5f;
    public float cameraSpeed = 0.02f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
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

    private void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
