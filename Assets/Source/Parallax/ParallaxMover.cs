using System;
using MyArchitecture;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BreakYourOwnGame.Parallax
{
    public class ParallaxMover : SerializedMonoBehaviour
    {
        [OdinSerialize] private FloatVariable moveSpeed;

        private Material mat;
        private void Start()
        {
            mat = GetComponent<MeshRenderer>().material;
        }

        private void Update()
        {
            Vector2 offset = mat.mainTextureOffset;
            offset.x += moveSpeed.Value * Time.deltaTime / 100f;
            mat.mainTextureOffset = offset;
        }
    }
}