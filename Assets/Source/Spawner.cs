using System;
using MyArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BreakYourOwnGame
{
    public class Spawner : SerializedMonoBehaviour
    {
        [FoldoutGroup("SOs for bool LR spawners")]
        [SerializeField] private BooleanVariable rightSpawner;
        [FoldoutGroup("SOs for bool LR spawners")]
        [SerializeField] private BooleanVariable leftSpawner;

        [FoldoutGroup("LR Spawner Transforms")]
        [SerializeField] private Transform rightSpawnObject;
        [FoldoutGroup("LR Spawner Transforms")]
        [SerializeField] private Transform leftSpawnObject;
        
        [FoldoutGroup("Obstacles")]
        [SerializeField] private GameObject obstacle;


        [SerializeField] private FloatVariable RMoveSpeed;
        [SerializeField] private FloatVariable LMoveSpeed;
        
        private float lastSpawnTime = 0f;
        private void FixedUpdate()
        {
            if (lastSpawnTime >= 1.6f)
            {
                Spawn();
                lastSpawnTime = 0f;
            }

            lastSpawnTime += Time.fixedDeltaTime;
        }

        private void Spawn()
        {
            if (rightSpawner.Value)
            {
                var temp = Instantiate(obstacle, rightSpawnObject.position, Quaternion.identity, rightSpawnObject);
                temp.GetComponent<MoveEntity>().moveSpeed = RMoveSpeed;
            }

            if (leftSpawner.Value)
            {
                var temp = Instantiate(obstacle, leftSpawnObject.position, Quaternion.identity, leftSpawnObject);
                temp.GetComponent<MoveEntity>().moveSpeed = LMoveSpeed;
            }
        }
    }
}