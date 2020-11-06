using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BreakYourOwnGame
{
    public class Grounded : SerializedMonoBehaviour
    {
        [OdinSerialize] private Transform feetTransform;
        [OdinSerialize] private Transform otherFeetTransform;
        [OdinSerialize] private LayerMask groundLayer;
        
        public bool IsGrounded { get; private set; }
        
        private Rigidbody2D rb;
        private Animator animController;
        
        private static readonly int CanJump = Animator.StringToHash("canJump");
        private static readonly int IsGrounded1 = Animator.StringToHash("isGrounded");

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animController = GetComponent<Animator>();
        }

        private void Update()
        {
            float dir = Mathf.Sign(rb.gravityScale);
            var temp = IsGrounded;

            if (CompareTag("Player"))
            {
                if(dir > 0)
                    IsGrounded = Physics2D.OverlapCircle(feetTransform.position, 0.5f, groundLayer);
                else if(dir < 0)
                    IsGrounded = Physics2D.OverlapCircle(otherFeetTransform.position, 0.5f, groundLayer);
            }
            else
                IsGrounded = Physics2D.OverlapCircle(feetTransform.position, 0.5f, groundLayer);
            
            
            if (temp != IsGrounded && CompareTag("Player"))
            {
                if (!IsGrounded)
                {
                    animController.SetBool(CanJump, true); //Just started jumping
                    animController.SetBool(IsGrounded1, false);
                }
                else
                {
                    animController.SetBool(IsGrounded1, true);
                    animController.SetBool(CanJump, false);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(feetTransform.position, 0.5f);
        }
    }
}