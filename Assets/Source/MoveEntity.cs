using System;
using Sirenix.OdinInspector;
using UnityEngine;
using MyArchitecture;

namespace BreakYourOwnGame
{
    public class MoveEntity : SerializedMonoBehaviour
    {
        [HideInInspector] public FloatVariable moveSpeed;

        private void Update()
        {
            transform.position += new Vector3(moveSpeed.Value * Time.deltaTime, 0, 0);
        }
    }
}