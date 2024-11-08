using UnityEngine;
using UnityEngine.Events;

namespace Framework.Gameplay
{
    public sealed class Score : MonoBehaviour
    {
        [SerializeField] private int score;
        [SerializeField] private int scoreThreshold;
        
        [SerializeField, Space(20)] private UnityEvent onScoreUpdate = new();
        [SerializeField] private UnityEvent onGameWon = new();
        [SerializeField] private UnityEvent onGameLose = new();

        /// <summary>
        /// Check if the win condition is met and call the correct unity event.
        /// </summary>
        public void CheckIfWon()
        {
            UnityEvent gameStateEvent = score >= scoreThreshold ? onGameWon : onGameLose;
            gameStateEvent?.Invoke();
        }
        
        public void IncreaseScore()
        {
            score++;
            onScoreUpdate?.Invoke();
        }

        public void DecreaseScore()
        {
            score--;
            onScoreUpdate?.Invoke();
        }

        public int GetScore() => score;
    }
}