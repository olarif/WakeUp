using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 30;
    public bool countdown = false;

    void Start()
    {
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }

    void Update()
    {
        if (!countdown && secondsLeft > 0)
        {
            StartCoroutine(Timer());
        }

        if (secondsLeft == 0)
        {
            FindObjectOfType<GameManager1>().EndGame();
        }
    }

    IEnumerator Timer()
    {
        countdown = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        }

        countdown = false;
    }

}
