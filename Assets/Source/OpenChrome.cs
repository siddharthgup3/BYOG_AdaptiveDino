using System;
using MyArchitecture;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;

namespace BreakYourOwnGame
{
    public class OpenChrome : SerializedMonoBehaviour
    {
        [OdinSerialize] private TextMeshProUGUI buttonText;
        [OdinSerialize] private BooleanVariable easterEgg;
        
        private const string clickMe = "Click Me!";
        private const string completeScore = "Reach 10k score to unlock";
        private void Start()
        {
            if (easterEgg.Value)
                buttonText.text = clickMe;
            else
                buttonText.text = completeScore;
        }

        public void OpenGame()
        {
            if(easterEgg.Value)
                Application.OpenURL("https://chromedino.com/");
        }
    }
}