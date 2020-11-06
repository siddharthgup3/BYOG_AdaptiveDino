using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BreakYourOwnGame
{
    public class FlipSprite : SerializedMonoBehaviour
    {
        private static SpriteRenderer _spriteRenderer;
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public static void FlipX()
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

        public static void FlipY()
        {
            _spriteRenderer.flipY = !_spriteRenderer.flipY;
        }
    }
}