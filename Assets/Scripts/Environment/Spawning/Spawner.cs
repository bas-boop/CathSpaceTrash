using UnityEngine;
using Random = UnityEngine.Random;

using Framework.Gameplay;

namespace Environment.Spawning
{
    public sealed class Spawner : MonoBehaviour
    {
        [SerializeField] private Score score;
        [SerializeField] private Rect spawnArea;
        [SerializeField] private Wave[] waves;

        private int _objectsDestroyed;
        private int _currentObjectToSpawn;
        private int _currentWave = -1;
        private int _totalObjects;

        private void Awake()
        {
            for (int i = 0; i < waves.Length; i++)
                for (int j = 0; j < waves[i].fallingObjects.Length; j++)
                    _totalObjects++;
        }

        private void Start() => SpawnNextWave();

        /// <summary>
        /// When there are no more spacecrafts left the lose condition is met.
        /// But the score should be updated to all other crew that are not saved.
        /// </summary>
        public void SetScoreCorrectAfterTotalDeath()
        {
            int objectsLeft = _totalObjects - _objectsDestroyed;
            
            for (int i = 0; i < objectsLeft; i++) 
                score.DecreaseScore();
        }

        /// <summary>
        /// Check if we have destroyed all objects
        /// </summary>
        public void Check()
        {
            _objectsDestroyed++;

            if (_objectsDestroyed == _totalObjects)
                score.CheckIfWon();
        }
        
        /// <summary>
        /// Looking at the correct wave and then spawning the next object.
        /// </summary>
        private void SpawnNextObject()
        {
            FallingObject instance = Instantiate(waves[_currentWave].fallingObjects[_currentObjectToSpawn],
                GetRandomSpawnPosition(), transform.rotation, transform);
            
            instance.Setup(this, score, score.IncreaseScore);
            _currentObjectToSpawn++;

            if (_currentObjectToSpawn != waves[_currentWave].fallingObjects.Length)
                return;
            
            CancelInvoke();
            SpawnNextWave();
        }

        /// <summary>
        /// Set the next wave to be the current one, if there is a next one.
        /// </summary>
        private void SpawnNextWave()
        {
            _currentObjectToSpawn = 0;
            _currentWave++;
            
            if (waves.Length == _currentWave)
                return;
            
            Wave currentWave = waves[_currentWave];
            InvokeRepeating(nameof(SpawnNextObject), currentWave.timeUntilSpawn, currentWave.spawningTime);
        }
        
        /// <summary>
        /// Get a random spawn position in the rect.
        /// </summary>
        /// <returns>Returns a random position within the rect.</returns>
        private Vector2 GetRandomSpawnPosition()
        {
            Vector2 randomSpawnPosition = new (Random.Range(-spawnArea.width, spawnArea.width) + transform.position.x,
                                            Random.Range(-spawnArea.height, spawnArea.height) + transform.position.y);
            return randomSpawnPosition;
        }
    }
}