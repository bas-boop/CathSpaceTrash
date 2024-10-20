using UnityEngine;
using UnityEngine.Events;

namespace Framework.Gameplay
{
    public sealed class Score : MonoBehaviour
    {
        [SerializeField] private int score;
        [SerializeField] private UnityEvent onScoreUpdate = new();

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