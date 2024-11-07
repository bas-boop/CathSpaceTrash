using System.Collections;
using UnityEngine;
using UnityEngine.Events;

using Framework.Customization;
using Player;

namespace Framework.Gameplay
{
    public sealed class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPosition;
        [SerializeField] private GameObject backupPlayer;
        [SerializeField] private int spaceCraftAmount = 1;
        [SerializeField] private float waitTimeToSpawnNewSpacecraft = 2;

        [Space(20), SerializeField] private UnityEvent onNoMoreSpaceCrafts = new();

        private void Start() => StartCoroutine(SpawnSpacecraft(true));

        public void SpawnNewSpaceCraft() => StartCoroutine(SpawnSpacecraft(false));

        /// <summary>
        /// Spawns the player, if no custamiztion has acourd the backup will be spawne.
        /// Can be delayed via the bool param.
        /// </summary>
        /// <param name="isFirstSpacecraft">Check if this the first spacecraft is.</param>
        /// <returns>Coroutine</returns>
        private IEnumerator SpawnSpacecraft(bool isFirstSpacecraft)
        {
            if (!isFirstSpacecraft)
                yield return new WaitForSeconds(waitTimeToSpawnNewSpacecraft);
            
            if (spaceCraftAmount == 0) 
                onNoMoreSpaceCrafts?.Invoke();
            
            GameObject savedShip = SkinManager.Instance.GetShip();
            GameObject spaceCraft = Instantiate(savedShip == null
                    ? backupPlayer 
                    : savedShip,
                playerSpawnPosition.position, playerSpawnPosition.rotation);

            SpawnEffect effect = spaceCraft.GetComponent<SpawnEffect>();
            effect.SetSpawner(this);
            
            if (!isFirstSpacecraft)
                effect.Flicker();

            spaceCraftAmount--;
        }
    }
}