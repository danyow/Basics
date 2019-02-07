using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{

    const float 
    // 每小时度数
        degreesPerHour = 30f, 
    // 每分钟度数
        degreesPerMinute = 6f, 
    // 每秒钟度数
        degreesPerSecond = 6f; 
    public bool continuous;

    public Transform hoursTransform, minutesTransform, secondsTransform;


    private void Update() {
        if (continuous) {
            UpdateContinuous();
        } else {
            UpdateDiscrete();
        }
    }

    // 连续的
    private void UpdateContinuous() {
        TimeSpan nowTime = DateTime.Now.TimeOfDay;
        hoursTransform.localRotation   = Quaternion.Euler(0f, (float)nowTime.TotalHours * degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, (float)nowTime.TotalMinutes * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, (float)nowTime.TotalSeconds * degreesPerSecond, 0f);
    }

    // 离散的
    private void UpdateDiscrete() {
        DateTime nowTime = DateTime.Now;
        hoursTransform.localRotation   = Quaternion.Euler(0f, nowTime.Hour * degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, nowTime.Minute * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, nowTime.Second * degreesPerSecond, 0f);
    }
}
