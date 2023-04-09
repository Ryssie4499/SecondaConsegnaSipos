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
    [Header("Audio")]
    public AudioSource powerUpAudio;

    [Header("PowerUp Type")]
    public TypeOfPowerUp type;

    //References
    GameManager GM;
    UIManager UM;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        UM = FindObjectOfType<UIManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GM.gameStatus == GameStatus.gameRunning && other.CompareTag("Player") && GM.shield == false)
        {
            if (type == TypeOfPowerUp.MaxRay)
            {
                powerUpAudio.Play();
                GM.ray = true;
                Destroy(gameObject);
            }
            else if (type == TypeOfPowerUp.Shield)
            {
                powerUpAudio.Play();
                UM.ShieldTimer.fillAmount = 1;
                GM.shield = true;
                Destroy(gameObject);
            }
            else if (type == TypeOfPowerUp.EnemyFreeze)
            {
                powerUpAudio.Play();
                UM.FreezeTimer.fillAmount = 1;
                GM.freeze = true;
                Destroy(gameObject);
            }
            else if (type == TypeOfPowerUp.EnemyBoost)
            {
                powerUpAudio.Play();
                UM.MalusTimer.fillAmount = 1;
                GM.malus = true;
                Destroy(gameObject);
            }
        }
    }
}
