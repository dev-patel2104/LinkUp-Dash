using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clock;
    [SerializeField] int timer;

    int min;
    int seconds;
    bool isOn;

    void Start()
    {
        clock = GetComponent<TextMeshProUGUI>();
        clock.text = "0:00";
        timer = min = seconds = 0;
        isOn = false;
    }

    
    void Update()
    {
        if (FindObjectsOfType<PlayerMovement>().Length >= 2 && !isOn)
        {
            isOn = true;
            StartCoroutine(timerController());
        }
    }

    IEnumerator timerController()
    {
        while(isOn)
        {
            timer++;
            if (timer % 60 == 0)
            {
                min++;
            }
            seconds = timer % 60;
            string minStr = min.ToString();
            string secStr = seconds.ToString();
            clock.text = minStr+":"+seconds;
            yield return new WaitForSeconds(1f);

        } 
    }
}
