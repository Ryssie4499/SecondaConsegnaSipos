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
    public TypeOfPowerUp type;
    GameManager GM;
    Enemy e;
    PlayerMovement pM;
    UIManager UM;
    private void Start()
    {
        e = FindObjectOfType<Enemy>();
        GM = FindObjectOfType<GameManager>();
        pM = FindObjectOfType<PlayerMovement>();
        UM = FindObjectOfType<UIManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == TypeOfPowerUp.MaxRay)
            {
                if (GM.shield == false)
                {
                    GM.ray = true;
                    Destroy(gameObject);
                }
            }
            else if (type == TypeOfPowerUp.Shield)
            {
                if (GM.shield == false)
                {
                    pM.shieldTimer = 5f;
                    UM.ShieldTimer.fillAmount = 1;
                    GM.shield = true;
                    Destroy(gameObject);
                }
            }
            else if (type == TypeOfPowerUp.EnemyFreeze)
            {
                if (GM.shield == false)
                {
                    UM.FreezeTimer.fillAmount = 1;
                    e.freezingTime = 5f;
                    GM.freeze = true;
                    Destroy(gameObject);
                }
            }
            else if (type == TypeOfPowerUp.EnemyBoost)
            {
                if (GM.shield == false)
                {
                    UM.MalusTimer.fillAmount = 1;
                    e.malusTime = 6f;
                    GM.malus = true;
                    Destroy(gameObject);
                }
            }

        }
    }
}
