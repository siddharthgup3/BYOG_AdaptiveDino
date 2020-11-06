using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BreakYourOwnGame
{
    public class Despawner : SerializedMonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(other.gameObject);
        }
    }
}