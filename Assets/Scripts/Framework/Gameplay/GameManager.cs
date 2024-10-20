using UnityEngine;

using Framework.Customization;

namespace Framework.Gameplay
{
    /// <summary>
    /// Game manager does miscellaneous small tasks that can be grouped for the gameplay.
    /// </summary>
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPosition;
        [SerializeField] private GameObject backupPlayer;
        
        /// <summary>
        /// Makes the falling object ignore collison with the walls and other falling objects.
        /// </summary>
        private void Awake()
        {
            Physics2D.IgnoreLayerCollision(7, 7, true);
            Physics2D.IgnoreLayerCollision(7, 6, true);
        }

        /// <summary>
        /// Spawns the player, if no custamiztion has acourd the backup will be spawned.
        /// </summary>
        private void Start()
        {
            GameObject ship = SkinManager.Instance.GetShip();
            Instantiate(ship == null
                    ? backupPlayer 
                    : ship,
                playerSpawnPosition.position, playerSpawnPosition.rotation);
        }
    }
}