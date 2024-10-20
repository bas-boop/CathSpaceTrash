using UnityEngine;

namespace Player
{
    public sealed class Turner : MonoBehaviour
    {
        [SerializeField] private Transform turnObject;
        [SerializeField] private float speed = 1;
        [SerializeField, Range(0, -180)] private float minRotation = -45f;
        [SerializeField, Range(0, 180)] private float maxRotation = 45f;
        
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
    }
}