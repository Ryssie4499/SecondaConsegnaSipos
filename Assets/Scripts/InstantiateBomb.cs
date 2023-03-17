using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBomb : MonoBehaviour
{
    public GameObject bomb;
    void Start()
    {

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
        }
    }
}
