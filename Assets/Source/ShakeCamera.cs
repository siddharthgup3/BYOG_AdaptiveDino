using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BreakYourOwnGame
{
    public class ShakeCamera : SerializedMonoBehaviour
    {
        [OdinSerialize] private float shakeDuration = 0.1f;
        [OdinSerialize] private float shakeMagnitude = 0.03f;
        [OdinSerialize] private float dampingSpeed = 1.0f;
        [OdinSerialize] private Vector3 initialPosition;
        private void OnEnable()
        {
            initialPosition = transform.localPosition;
        }

        private void Update()
        {
            if (shakeDuration > 0)
            {
                Debug.Log($"Shaking, duration remaining = {shakeDuration}");
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
                shakeDuration -= Time.deltaTime * dampingSpeed;
            }

            if (shakeDuration <= 0) transform.localPosition = initialPosition;
            
        }
        
        public void TriggerShake(float D) //call this function in scripts that require the camera to be shaken.
        {
            Debug.Log($"New shake");
            shakeDuration = D;
        }
    }
}