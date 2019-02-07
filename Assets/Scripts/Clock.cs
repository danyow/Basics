using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{

    // 每小时度数
    const float 
        degreesPerHour = 30f, 
        degreesPerMinute = 6f, 
        degreesPerSecond = 6f; 
    // 每分钟度数

    // 每秒钟度数
    public Transform hoursTransform, minutesTransform, secondsTransform;

    private void Update() {
        DateTime nowTime = DateTime.Now;
        hoursTransform.localRotation   = Quaternion.Euler(0f, nowTime.Hour * degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, nowTime.Minute * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, nowTime.Second * degreesPerSecond, 0f);
    }
}
