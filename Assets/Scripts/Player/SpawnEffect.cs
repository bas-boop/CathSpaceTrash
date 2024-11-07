using System.Collections;
using UnityEngine;
using UnityEngine.Events;

using Framework.Gameplay;

namespace Player
{
    public sealed class SpawnEffect : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer render;
        [SerializeField, Range(1, 15)] private float flickerSpeed = 1;
        [SerializeField] private float timeToFlickerFor = 3;

        [Space, SerializeField] private UnityEvent onStartFlicker = new();
        [SerializeField] private UnityEvent onDoneFlicker = new();

        private PlayerSpawner _spawner;

        private void OnDestroy() => _spawner.SpawnNewSpaceCraft();

        public void SetSpawner(PlayerSpawner parent) => _spawner = parent;

        public void Flicker()
        {
            onStartFlicker?.Invoke();
            StartCoroutine(FlickerRoutine());
        }

        private IEnumerator FlickerRoutine()
        {
            float endTime = Time.time + timeToFlickerFor;
            
            while (Time.time < endTime)
            {
                // Calculate alpha based on sine wave for smooth flicker and apply that
                float alpha = Mathf.Abs(Mathf.Sin(Time.time * flickerSpeed));
                Color color = render.color;
                color.a = alpha;
                render.color = color;

                yield return null;
            }
            
            Color finalColor = render.color;
            finalColor.a = 1;
            render.color = finalColor;
            onDoneFlicker?.Invoke();
        }
    }
}