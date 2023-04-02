using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfPowerUp
{
    MaxRay,
    Shield,
    EnemyFreeze,
    EnemyBoost
}
public class PowerUp : MonoBehaviour
{
    Enemy eM;
    public TypeOfPowerUp type;
    GameManager GM;
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        eM = FindObjectOfType<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == TypeOfPowerUp.MaxRay)
            {
                GM.ray = true;
            }
            else if (type == TypeOfPowerUp.Shield)
            {
                GM.shield = true;
            }
            else if(type == TypeOfPowerUp.EnemyFreeze)
            {
                GM.freeze = true;
            }
            else if(type == TypeOfPowerUp.EnemyBoost)
            {
                GM.malus = true;
            }
            Destroy(gameObject);
        }
    }
}
