using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BreakYourOwnGame
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayBackgroundMusic : SerializedMonoBehaviour
    {
        [OdinSerialize] private AudioSource audioSource;
        [OdinSerialize] private AudioClip backgroundClip;

        private void Start()
        {
            audioSource.clip = backgroundClip;
            audioSource.Play();
        }
    }
}