using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvas.Gameplay
{
    public sealed class CompletePanel : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private GameObject winText;
        [SerializeField] private GameObject loseText;
        [SerializeField] private Color winColor = Color.green;
        [SerializeField] private Color loseColor = Color.red;

        public void Complete(bool hasWon)
        {
            background.color = hasWon ? winColor : loseColor;
            GameObject textToShow = hasWon ? winText : loseText;
            textToShow.SetActive(true);
        }
    }
}