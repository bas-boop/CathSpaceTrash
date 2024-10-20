using UnityEngine;

namespace Framework.Customization
{
    /// <summary>
    /// Holder of the chosen ship and flame for the player visual.
    /// </summary>
    public sealed class SkinManager : Singleton<SkinManager>
    {
        [SerializeField] private GameObject[] ships;
        [SerializeField] private Sprite[] sprites;

        private int _selectedShip;
        private int _selectedFlame;

        public void SetShip(int target) => _selectedShip = target;

        public void SetFlame(int target) => _selectedFlame = target;

        public GameObject GetShip() => ships?[_selectedShip];

        public Sprite GetFlame() => sprites?[_selectedFlame];
    }
}