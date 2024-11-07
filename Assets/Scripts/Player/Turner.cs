using System.Collections;
using UnityEngine;

namespace Player
{
    public sealed class Turner : MonoBehaviour
    {
        [SerializeField] private Transform turnObject;
        [SerializeField, Range(1, 500)] private float speed = 1;
        [SerializeField, Range(1, 500)] private float resetSpeed = 0.1f;
        [SerializeField, Range(0, -180)] private float minRotation = -45f;
        [SerializeField, Range(0, 180)] private float maxRotation = 45f;
        
        private Coroutine _resetRotationCoroutine;
        
        /// <summary>
        /// Will turn the player to the direction given.
        /// </summary>
        /// <param name="input">The input to turn left or right.</param>
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

        /// <summary>
        /// Makes a smooth return to the start rotation.
        /// </summary>
        public void ResetRotation()
        {
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

            // Rotate towards 0 at a constant speed
            while (Mathf.Abs(currentZRotation) > 0.01f)
            {
                currentZRotation = turnObject.localEulerAngles.z;

                if (currentZRotation > 180f)
                    currentZRotation -= 360f;

                // Rotate by a fixed step towards 0 at a constant speed
                float step = resetSpeed * Time.deltaTime;
                float newZRotation = Mathf.MoveTowards(currentZRotation, 0, step);

                // Apply the new rotation
                turnObject.localEulerAngles = new (turnObject.localEulerAngles.x, turnObject.localEulerAngles.y, newZRotation);

                yield return null;  // Wait for the next frame
            }

            // Ensure the final rotation is exactly 0 after resetting
            turnObject.localEulerAngles = new (turnObject.localEulerAngles.x, turnObject.localEulerAngles.y, 0);
        }
    }
}