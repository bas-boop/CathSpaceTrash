using UnityEngine;

namespace UI.Canvas.Menu
{
    /// <summary>
    /// A little animation with scale and color for the title of the game.
    /// </summary>
    public sealed class Title : MonoBehaviour
    {
        [SerializeField] private float animationSpeed = 2f;
        [SerializeField, Tooltip("The amount to grow in percentage.")] private float scaleMagnitude = 0.1f;
        [SerializeField] private Color startColor = Color.white;
        [SerializeField] private Color endColor = Color.gray;
        
        private Vector3 _originalScale;
        private CanvasRenderer _canvasRenderer;

        /// <summary>
        /// Setup for the animation and color.
        /// </summary>
        private void Awake()
        {
            _canvasRenderer = GetComponent<CanvasRenderer>();
            _originalScale = transform.localScale;
            _canvasRenderer.SetColor(startColor);
        }

        private void Update() => AnimateTitle();

        private void AnimateTitle()
        {
            // Animate scale
            float scaleFactor = 1 + Mathf.PingPong(Time.time * animationSpeed, scaleMagnitude);
            transform.localScale = _originalScale * scaleFactor;
            
            // Fade color
            float colorFactor = Mathf.PingPong(Time.time * animationSpeed, 1);
            Color newColor = Color.Lerp(startColor, endColor, colorFactor);
            _canvasRenderer.SetColor(newColor);
        }
    }
}