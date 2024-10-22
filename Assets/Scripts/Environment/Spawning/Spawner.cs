using UnityEngine;

using Framework.Gameplay;

namespace Environment.Spawning
{
    public sealed class Spawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Score score;
        [SerializeField] private BaseFallingObject[] fallingObjects;
        [Header("Settings")]
        [SerializeField] private float waitUntilStartSpawning = 1;
        [SerializeField] private float spawnTime = 1;
        [SerializeField] private Rect spawnArea;

        private int _objectsDestroyed;
        private int _currentObjectToSpawn;
        
        private void Start() => InvokeRepeating(nameof(SpawnNextObject), waitUntilStartSpawning, spawnTime);

        public void Check()
        {
            _objectsDestroyed++;

            if (_objectsDestroyed == fallingObjects.Length) 
                score.CheckIfWon();
        }
        
        private void SpawnNextObject()
        {
            BaseFallingObject instance = Instantiate(fallingObjects[_currentObjectToSpawn], GetRandomSpawnPosition(),
                                                        transform.rotation, transform);
            
            instance.Setup(this, score, score.IncreaseScore);
            _currentObjectToSpawn++;

            if (_currentObjectToSpawn == fallingObjects.Length)
                CancelInvoke();
        }

        private Vector2 GetRandomSpawnPosition()
        {
            Vector2 randomSpawnPosition = new (Random.Range(-spawnArea.width, spawnArea.width) + transform.position.x,
                                            Random.Range(-spawnArea.height, spawnArea.height) + transform.position.y);
            return randomSpawnPosition;
        }
    }
}