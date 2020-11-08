using System;
using Sirenix.OdinInspector;
using UnityEngine;
using MyArchitecture;

namespace BreakYourOwnGame
{
    public class MoveEntity : SerializedMonoBehaviour
    {
        [HideInInspector] public FloatVariable moveSpeed;

        [HideInInspector] public Spawner _spawner;

        private void Update()
        {
            transform.position += new Vector3(moveSpeed.Value * Time.deltaTime, 0, 0);
        }

        private void OnDisable()
        {
            if (_spawner.leftSpawnedObstacles.Contains(this))
            {
                _spawner.leftSpawnedObstacles.Remove(this);
            }
            else if (_spawner.rightSpawnedObstacles.Contains(this))
            {
                _spawner.rightSpawnedObstacles.Remove(this);
            }
        }
    }
}