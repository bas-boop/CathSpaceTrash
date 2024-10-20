using Framework.Customization;
using UnityEngine;

namespace UI.Canvas.Skins
{
    public sealed class GiveOptions : MonoBehaviour
    {
        public void SetShip(int target) => SkinManager.Instance.SetShip(target);

        public void SetFlame(int target) => SkinManager.Instance.SetFlame(target);
    }
}