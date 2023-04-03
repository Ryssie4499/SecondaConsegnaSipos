using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool ray;
    [HideInInspector] public bool shield;
    [HideInInspector] public bool freeze;
    [HideInInspector] public bool malus;
    public float weaponRange = 1f;
    //public GameObject[] powerUp;
    //private void Update()
    //{
    //    if(shield == true)
    //    {
    //        foreach(GameObject pU in powerUp)
    //        {
    //            pU.SetActive(false);
    //        }
    //    }
    //    else
    //    {
    //        foreach (GameObject pU in powerUp)
    //        {
    //            pU.SetActive(true);
    //        }
    //    }    
    //}
}
