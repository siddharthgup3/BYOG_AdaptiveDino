using System;
using MyArchitecture;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BreakYourOwnGame
{
    public class InputHandler : SerializedMonoBehaviour
    {
        
        [OdinSerialize] private KeyCode keyCodeTrigger = KeyCode.Space;
        [OdinSerialize] private BooleanVariable canPlayerJump;
        [OdinSerialize] private BooleanVariable canObstacleJump;

        private GameEnforcer _enforcer;
        private Grounded player;

        public delegate bool Jump();

        public static event Jump playerJump;
        public static event Jump obstacleJump;

        private void Start()
        {
            _enforcer = GetComponent<GameEnforcer>();
            player = GameObject.FindWithTag("Player").GetComponent<Grounded>();
        }

        private void Update()
        {
            if (!Input.GetKeyDown(keyCodeTrigger))
                return;


            if (canObstacleJump.Value)
            {
                obstacleJump?.Invoke();
                var temp = GameObject.FindGameObjectsWithTag("Obstacle");
                foreach (var obstacle in temp)
                {
                    if (obstacle.GetComponent<Grounded>().IsGrounded)
                    {
                        int roll = Random.Range(0, 3);
                        if (roll == 1)
                        {
                            _enforcer.Invoke(nameof(GameEnforcer.SwitchMode), Time.timeScale / 2f);
                        }
                        break;
                    }
                }
            }

            if (canPlayerJump.Value)
            {
                playerJump?.Invoke();
                if (player.IsGrounded)
                {
                    int roll = Random.Range(0, 3);
                    if (roll == 1)
                    {
                        _enforcer.Invoke(nameof(GameEnforcer.SwitchMode), Time.timeScale / 2f);
                    }
                }
            }
        }
    }
}