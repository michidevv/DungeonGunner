using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sfx
{
    BoxBreaking = 0,
    EnemyDeath,
    EnemyHurt,
    Explosion,
    Impact,
    PickupCoin,
    PickupGun,
    PickupHealth,
    PlayerDash,
    PlayerDeath,
    PlayerDie,
    PlayerHurt,
    Shoot1,
    Shoot2,
    Shoot3,
    Shoot4,
    Shoot5,
    Shoot6,
    ShopBuy,
    ShopNotEnough,
    WarpOut
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    AudioSource levelMusic, gameOverMusic, victoryMusic;
    [SerializeField]
    AudioSource[] sfx;

    private void Awake()
    {
        instance = this;
    }

    public void PlayGameOver()
    {
        levelMusic.Stop();

        gameOverMusic.Play();
    }

    public void PlayLevelWin()
    {
        levelMusic.Stop();

        victoryMusic.Play();
    }

    public void PlaySfx(Sfx effect)
    {
        int index = (int) effect;
        sfx[index].Stop();
        sfx[index].Play();
    }
}
