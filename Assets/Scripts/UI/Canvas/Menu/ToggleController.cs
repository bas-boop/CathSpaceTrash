using UnityEngine;
using UnityEngine.UI;

using Framework;

namespace UI.Canvas.Menu
{
    public sealed class ToggleController : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        private void Awake() => ApplyCorrectSetting();

        public void Toggle(bool target) => PlayerSettings.Instance.IsUsingController = target;

        private void ApplyCorrectSetting()
        {
            if (!toggle)
                toggle = GetComponent<Toggle>();
            
            toggle.isOn = PlayerSettings.Instance.IsUsingController;
        }
    }
}