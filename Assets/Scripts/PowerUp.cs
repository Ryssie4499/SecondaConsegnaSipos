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
    AudioManager AM;

    private void Start()
    {
        e = FindObjectOfType<Enemy>();
        GM = FindObjectOfType<GameManager>();
        pM = FindObjectOfType<PlayerMovement>();
        UM = FindObjectOfType<UIManager>();
        AM = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GM.gameStatus == GameStatus.gameRunning)
        {
            if (other.CompareTag("Player"))
            {
                if (type == TypeOfPowerUp.MaxRay)
                {
                    if (GM.shield == false)
                    {
                        AM.maxRay.Play();
                        GM.ray = true;
                        Destroy(gameObject);
                    }
                }
                else if (type == TypeOfPowerUp.Shield)
                {
                    if (GM.shield == false)
                    {
                        AM.shield.Play();
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
                        AM.freeze.Play();
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
                        AM.malus.Play();
                        UM.MalusTimer.fillAmount = 1;
                        e.malusTime = 6f;
                        GM.malus = true;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
