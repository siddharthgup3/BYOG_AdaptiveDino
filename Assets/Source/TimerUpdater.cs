using System;
using System.Collections;
using System.Collections.Generic;
using MyArchitecture;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;

namespace BreakYourOwnGame
{
    public class TimerUpdater : SerializedMonoBehaviour
    {
        [SerializeField] private AudioClip levelUp;
        [OdinSerialize] private BooleanVariable easterEgg;
        
        [ShowInInspector, ReadOnly] private TextMeshProUGUI scoreText;
        [ShowInInspector, ReadOnly] private uint actualScore;

        private float timeSinceStart;
        private void Awake()
        {
            scoreText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            timeSinceStart += Time.deltaTime;
            actualScore = (uint) (timeSinceStart * 100);

            if (actualScore >= 10000)
            {
                easterEgg.Value = true;
            }
            
            if (actualScore % 1000 == 0 && actualScore > 0)
            {
                Sound.audSource.PlayOneShot(levelUp, 0.5f);
                Time.timeScale += Time.timeScale / 10f;
            }

            string temp = "";
            for (int i = 0; i < 7 - actualScore.ToString().Length; i++)
            {
                temp += "0";
            }
            scoreText.text = temp + actualScore;
        }
    }
}