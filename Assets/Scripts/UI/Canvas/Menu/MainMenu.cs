using UnityEngine;

namespace UI.Canvas.Menu
{
    /// <summary>
    /// Main menu specifically button options.
    /// </summary>
    public sealed class MainMenu : MonoBehaviour
    {
        private const string GITHUB_REPO = "https://github.com/bas-boop/CathSpaceTrash";
        
        public void OpenGithub() => Application.OpenURL(GITHUB_REPO);

        public void Quit() => Application.Quit();
    }
}