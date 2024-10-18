using UnityEngine;

using Framework.Attributes;
using Framework.Extensions;

namespace UI.Canvas.Menu
{
    /// <summary>
    /// A simple up and down motion for the animated background of the main menu.
    /// With some speed and height settings.
    /// </summary>
    public sealed class Bobbing : MonoBehaviour
    {
        [SerializeField, RangeVector2(0, 10, 0, 10)] private Vector2 speed = Vector2.one;
        [SerializeField] private float height = 0.1f;
        
        private Vector3 _startPos;
        private float _speed;

        /// <summary>
        /// Get the start position of this object and set a speed between the random values.
        /// </summary>
        private void Awake()
        {
            _startPos = transform.position;
            _speed = Random.Range(speed.x, speed.y);
        }

        /// <summary>
        /// Update the up and down monition, only on the y-axis.
        /// </summary>
        private void Update()
        {
            Vector3 targetPosition = transform.position;
            targetPosition.SetY(_startPos.y + Mathf.Sin(Time.time * _speed) * height);
            transform.position = targetPosition;
        }
    }
}