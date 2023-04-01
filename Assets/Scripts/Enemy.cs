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
        RaycastHit hit;
        if(Physics.Raycast(transform.position, rb.velocity, out hit, range)&&hit.transform.name!="Player")
        {
            direction = Random.Range(0, 4);
        }
        
       
    }
   
}
