using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyArchitecture
{
    public class TimerMono : MonoBehaviour
    {
        private static List<Timer> myCurrentTimers;

        private void Start()
        {
            myCurrentTimers = new List<Timer>();
        }

        private void Update()
        {
            foreach (var Timer in myCurrentTimers)
            {
                Timer.alarmTime -= Time.deltaTime;
                if (Timer.alarmTime <= 0)
                    RemoveTimerAndCallCallbacks(Timer);
            }
        }

        private void RemoveTimerAndCallCallbacks(Timer finishedTimer)
        {
            myCurrentTimers.Remove(finishedTimer);
            //Call the callback function HOW
            finishedTimer.callbackfunctions?.Invoke();
        }
    }

    public class Timer
    {
        public float alarmTime;

        public delegate void callbackDelegate();

        public callbackDelegate callbackfunctions;
        
        public Timer(float alarmTime, callbackDelegate callback)
        {
            this.alarmTime = alarmTime;
            this.callbackfunctions += callback;
        }
    }
}