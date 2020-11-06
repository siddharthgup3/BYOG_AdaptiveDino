using System;
using System.Collections.Generic;
using MyArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BreakYourOwnGame
{
    public class Spawner : SerializedMonoBehaviour
    {
        [FoldoutGroup("SOs for bool LR spawners")] [SerializeField]
        private BooleanVariable rightSpawner;

        [FoldoutGroup("SOs for bool LR spawners")] [SerializeField]
        private BooleanVariable leftSpawner;

        [FoldoutGroup("LR Spawner Transforms")] [SerializeField]
        private Transform rightSpawnObject;

        [FoldoutGroup("LR Spawner Transforms")] [SerializeField]
        private Transform leftSpawnObject;

        [FoldoutGroup("Obstacles")] [SerializeField]
        private GameObject obstacle;


        [SerializeField] public FloatVariable RMoveSpeed;
        [SerializeField] public FloatVariable LMoveSpeed;

        private float lastSpawnTime = 0f;

        [ShowInInspector, ReadOnly] public  List<MoveEntity> rightSpawnedObstacles;
        [ShowInInspector, ReadOnly] public  List<MoveEntity> leftSpawnedObstacles;

        private void Start()
        {
            rightSpawnedObstacles = new List<MoveEntity>();
            leftSpawnedObstacles = new List<MoveEntity>();
        }

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
                var instantiatedObstacle = Instantiate(obstacle, rightSpawnObject.position, Quaternion.identity,
                    rightSpawnObject);
                var moveEntity = instantiatedObstacle.GetComponent<MoveEntity>();
                moveEntity.moveSpeed = RMoveSpeed;
                moveEntity._spawner = this;
                rightSpawnedObstacles.Add(moveEntity);
            }

            if (leftSpawner.Value)
            {
                var instantiatedObstacle =
                    Instantiate(obstacle, leftSpawnObject.position, Quaternion.identity, leftSpawnObject);
                var moveEntity = instantiatedObstacle.GetComponent<MoveEntity>();
                moveEntity.moveSpeed = LMoveSpeed;
                moveEntity._spawner = this;
                leftSpawnedObstacles.Add(moveEntity);
            }
        }
    }
}