using UnityEngine;

namespace Framework.Gameplay
{
    public sealed class CollisionIgnorer : MonoBehaviour
    {
        /// <summary>
        /// Makes the falling object ignore collision with the walls and other falling objects.
        /// </summary>
        private void Awake()
        {
            Physics2D.IgnoreLayerCollision(7, 7, true);
            Physics2D.IgnoreLayerCollision(7, 6, true);
        }
    }
}