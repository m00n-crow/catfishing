using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

//lässt es zu das die Zeit im Spiel angezeigt wird
public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private void OnEnable()

    // Start is called before the first frame update
    
    {
        // Fill text element on game start
        timeText.text = "Time: 10:00";
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        timeText.text = $"Time: {TimeManager.Hour}:{TimeManager.Minute:00}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
