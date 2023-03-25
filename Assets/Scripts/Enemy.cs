using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    public int direction;
    public float range;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = Random.Range(0, 4);
    }

    void Update()
    {
        Mathf.RoundToInt(gameObject.transform.position.x);
        Mathf.RoundToInt(gameObject.transform.position.y);
        switch (direction)
        {
            case 0:
                rb.velocity = Vector3.up * speed;
                break;
            case 1:
                rb.velocity = Vector3.down * speed;
                break;
            case 2:
                rb.velocity = Vector3.right * speed;
                break;
            case 3:
                rb.velocity = Vector3.left * speed;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            RaycastHit hit;
            //right
            if (Physics.Raycast(transform.position, transform.right, out hit, range))
            {
                direction = Random.Range(0, 2) | 3 ;
            }
            //left
            if (Physics.Raycast(transform.position, -transform.right, out hit, range))
            {
                direction = Random.Range(0, 3);
            }
            //up
            if (Physics.Raycast(transform.position, transform.up, out hit, range))
            {
                direction = Random.Range(1, 4);
            }
            //down
            if (Physics.Raycast(transform.position, -transform.up, out hit, range))
            {
                direction = Random.Range(2, 4)|0;
            }
        }
    }
}
