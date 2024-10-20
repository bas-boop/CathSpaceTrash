using UnityEngine;

using Framework.Extensions;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Movement : MonoBehaviour
    {
        [SerializeField, Range(1, 25)] private float speed = 1;
        [SerializeField, Range(0.1f, 10f)] private float acceleration = 2f; 
        [SerializeField, Range(0.1f, 10f)] private float deceleration = 2f; 
        
        private Rigidbody2D _rigidbody2D;
        private Vector2 _currentVelocity;
        
        // This is how I get a require component, is there a better way?
        private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

        public void Move(Vector2 input)
        {
            // change the velocity to the speed value or nothing, depending on input
            _currentVelocity = input.magnitude > 0 
                ? Vector2.MoveTowards(_currentVelocity, input * speed, acceleration * Time.deltaTime)
                : Vector2.MoveTowards(_currentVelocity, Vector2.zero, deceleration * Time.deltaTime);

            _currentVelocity.SetY(0);
            _rigidbody2D.velocity = _currentVelocity;
        }
    }
}