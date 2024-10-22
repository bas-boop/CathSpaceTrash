using System.Collections;
using UnityEngine;

namespace Player
{
    public sealed class Turner : MonoBehaviour
    {
        [SerializeField] private Transform turnObject;
        [SerializeField] private float speed = 1;
        [SerializeField] private float resetSpeed = 0.1f;
        [SerializeField, Range(0, -180)] private float minRotation = -45f;
        [SerializeField, Range(0, 180)] private float maxRotation = 45f;
        
        private Coroutine _resetRotationCoroutine;
        
        public void Turn(float input)
        {
            if (input == 0)
                return;
            
            float currentZRotation = turnObject.localEulerAngles.z;

            // Handle cases where rotation exceeds 180 degrees due to how Unity represents angles (0 to 360)
            if (currentZRotation > 180f) 
                currentZRotation -= 360f;

            // Calculate the new rotation, -1 is to rotate with the right key
            float newZRotation = currentZRotation + input * speed * Time.deltaTime * -1;
            newZRotation = Mathf.Clamp(newZRotation, minRotation, maxRotation);
            
            turnObject.localEulerAngles = new (turnObject.localEulerAngles.x, turnObject.localEulerAngles.y, newZRotation);
        }

        public void ResetRotation()
        {
            // Stop any ongoing reset coroutine before starting a new one
            if (_resetRotationCoroutine != null)
                StopCoroutine(_resetRotationCoroutine);

            _resetRotationCoroutine = StartCoroutine(SmoothResetRotation());
        }

        private IEnumerator SmoothResetRotation()
        {
            float currentZRotation = turnObject.localEulerAngles.z;

            // Normalize rotation to -180 to 180 range
            if (currentZRotation > 180f)
                currentZRotation -= 360f;

            // Smoothly interpolate the rotation back to 0
            while (Mathf.Abs(currentZRotation) > 0.01f)
            {
                currentZRotation = turnObject.localEulerAngles.z;

                if (currentZRotation > 180f)
                    currentZRotation -= 360f;

                float newZRotation = Mathf.LerpAngle(currentZRotation, 0, resetSpeed * Time.deltaTime);
                turnObject.localEulerAngles = new (turnObject.localEulerAngles.x, turnObject.localEulerAngles.y, newZRotation);

                yield return null;
            }

            // Ensure it's set exactly to 0 after smoothing
            turnObject.localEulerAngles = new (turnObject.localEulerAngles.x, turnObject.localEulerAngles.y, 0);
        }
    }
}