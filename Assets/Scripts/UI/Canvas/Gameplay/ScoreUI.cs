using TMPro;
using UnityEngine;

using Framework.Gameplay;

namespace UI.Canvas.Gameplay
{
    public sealed class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Score scoreSystem;
        [SerializeField] private int crewMultiplier = 1;

        private void Start() => UpdateText();

        public void UpdateText() => scoreText.text = $"You have saved {scoreSystem.GetScore() * crewMultiplier} crew";
    }
}