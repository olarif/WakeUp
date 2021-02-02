using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeStart;
    public Text textBox;

    void Start()
    {
        textBox.text = timeStart.ToString("F1");
    }

    void Update()
    {
        timeStart += Time.deltaTime;
        textBox.text = timeStart.ToString("F1");
    }
}
