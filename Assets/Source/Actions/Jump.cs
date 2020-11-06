using System;
using MyArchitecture;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BreakYourOwnGame
{
    public class Jump : SerializedMonoBehaviour
    {
        [OdinSerialize] private float jumpForce;
        [OdinSerialize] private AudioClip jumpClip;

        [ShowInInspector, ReadOnly] private Grounded groundScript;

        private Rigidbody2D rb;
        private string entityTag;
        private GameEnforcer _gameEnforcer;

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody2D>();
            groundScript = GetComponent<Grounded>();
            entityTag = gameObject.tag;
            switch (entityTag)
            {
                case "Player":
                    InputHandler.playerJump += PerformJump;
                    break;
                case "Obstacle":
                    InputHandler.obstacleJump += PerformJump;
                    break;
                default:
                    Debug.Log($"TAG = {entityTag}");
                    throw new Exception("Wrong object");
            }
        }


        private bool PerformJump()
        {
            if (rb.gravityScale > 0 && groundScript.IsGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Sound.audSource.PlayOneShot(jumpClip, 0.5f);
            }
            else if (rb.gravityScale < 0 && groundScript.IsGrounded)
            {
                rb.AddForce(-Vector2.up * jumpForce, ForceMode2D.Impulse);
                Sound.audSource.PlayOneShot(jumpClip, 0.5f);
            }
            else
                return false;

            return true;
        }

        private void OnDisable()
        {
            switch (entityTag)
            {
                case "Player":
                    InputHandler.playerJump -= PerformJump;
                    break;
                case "Obstacle":
                    InputHandler.obstacleJump -= PerformJump;
                    break;
                default: throw new Exception("Wrong object");
            }
        }
    }
}