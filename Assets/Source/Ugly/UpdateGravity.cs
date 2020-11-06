using System;
using MyArchitecture;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BreakYourOwnGame
{
    public class UpdateGravity : SerializedMonoBehaviour
    {
        [OdinSerialize] private BooleanVariable isPosGravity;
        
        private Rigidbody2D rb;
        private float startingGravity;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            startingGravity = rb.gravityScale;
        }

        private void Update()
        {
            if (isPosGravity.Value)
                rb.gravityScale = startingGravity;
            else
                rb.gravityScale = -startingGravity;
        }
    }
}