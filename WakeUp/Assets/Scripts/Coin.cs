using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue;
    FMOD.Studio.EventInstance CoinSound;
    private string path = "event:/GetCoins";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ScoreManager.instance.ChangeScore(coinValue);
            GetCoinSound();
        }
    }

    void GetCoinSound()
    {
        FMOD.Studio.EventInstance CoinSound= FMODUnity.RuntimeManager.CreateInstance(path);
        CoinSound.start();
        
    }
    private void OnDestroy()
    {
        CoinSound.release();
    }

}
