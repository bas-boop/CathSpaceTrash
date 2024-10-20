using UnityEngine;

using Framework.Gameplay.ShootSystem;

namespace Player
{
    public sealed class Shooting : MonoBehaviour
    {
        [SerializeField] private Bullet bullet;
        [SerializeField, Range(0, 2)] private float shootInterval = 0.5f;
        [SerializeField] private Transform[] firePoints;

        private bool _isShooting;
        private bool _canShoot = true;

        /// <summary>
        /// Will shoot the bullet prefab with each fire point in firePoints.
        /// Does Instantiation, will be reworked later when there is object pooling.
        /// </summary>
        public void ActivateShoot()
        {
            if (_isShooting
                || !_canShoot)
                return;
            
            Shoot();
        }

        /// <summary>
        /// Shoot from every fire point a bullet.
        /// </summary>
        private void Shoot()
        {
            _isShooting = true;
            int l = firePoints.Length;
            
            for (int i = 0; i < l; i++) 
                Instantiate(bullet, firePoints[i].position, firePoints[i].rotation);
            
            Invoke(nameof(Reset), shootInterval);
        }

        /// <summary>
        /// Reset bools to shoot again.
        /// </summary>
        private void Reset()
        {
            _canShoot = true;
            _isShooting = false;
        }
    }
}