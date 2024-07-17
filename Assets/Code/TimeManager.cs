using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// Managed die Zeit die vergeht und greift dabei auf die Time UI zu (bzw die UI orientiert sich an den werten im manager)
public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    
    public static int Minute { get; private set; }
    public static int Hour { get; private set; }
    public int hour;

    private float minuteToRealTime = 1.5f;
    private float timer;

    void Start()
    {
        Minute = 00;
        Hour = 10;
        timer = minuteToRealTime;
    }

    // Update is called once per frame
    void Update()
    {
        hour = Hour;
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();
            if(Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();
                if(Hour >= 24)
                {
                    Hour = 0;
                }
            }
            timer = minuteToRealTime;
        }
    }
}
