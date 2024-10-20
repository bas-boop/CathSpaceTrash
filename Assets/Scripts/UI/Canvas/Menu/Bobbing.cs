using UnityEngine;

using Framework.Attributes;
using Framework.Extensions;

namespace UI.Canvas.Menu
{
    /// <summary>
    /// A simple up and down motion for the animated background of the main menu.
    /// With some speed and height settings.
    /// (Also used by the player's spacecraft flames, yes that is not UI)
    /// </summary>
    public sealed class Bobbing : MonoBehaviour
    {
        [SerializeField] private bool hasParent;
        [SerializeField, RangeVector2(0, 10, 0, 10)] private Vector2 speed = Vector2.one;
        [SerializeField] private float height = 0.1f;
        
        private Vector3 _startPosLocal;
        private float _speed;

        /// <summary>
        /// Get the start position of this object and set a speed between the random values.
        /// </summary>
        private void Awake()
        {
            _speed = Random.Range(speed.x, speed.y);
            _startPosLocal = hasParent 
                ? transform.localPosition 
                : transform.position;
        }

        /// <summary>
        /// Update the up and down monition, only on the y-axis.
        /// </summary>
        private void Update()
        {
            Vector3 targetPosition = _startPosLocal;
            targetPosition.SetY(_startPosLocal.y + Mathf.Sin(Time.time * _speed) * height);
            
            if (hasParent)
                transform.localPosition = targetPosition;
            else
                transform.position = targetPosition;
        }
    }
}