using UnityEngine;

using Framework.ObjectPooling;

namespace Player
{
    public sealed class Shooting : MonoBehaviour
    {
        [SerializeField] private BaseObjectPool bulletPool;
        [SerializeField] private BaseObjectPool rocketPool;
        [SerializeField, Range(0, 2)] private float shootIntervalBullet = 0.5f;
        [SerializeField, Range(0, 2)] private float shootIntervalRocket = 1f;
        [SerializeField] private Transform[] firePoints;

        private BaseObjectPool _currentPool;
        private float _currentShootInterval;
        
        private bool _isShooting;
        private bool _canShoot = true;

        /// <summary>
        /// Sets the default values for shoot interval and bullet to shoot.
        /// </summary>
        private void Awake()
        {
            _currentPool = bulletPool;
            _currentShootInterval = shootIntervalBullet;
        }

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
        /// This will switch the current pool to shoot form to the other pool. Also, the shoot speed will be switched.
        /// </summary>
        public void SwitchBullet()
        {
            if (_currentPool == bulletPool)
            {
                _currentPool = rocketPool;
                _currentShootInterval = shootIntervalRocket;
            }
            else
            {
                _currentPool = bulletPool;
                _currentShootInterval = shootIntervalBullet;
            }
        }

        /// <summary>
        /// Shoot from every fire point a bullet.
        /// </summary>
        private void Shoot()
        {
            _isShooting = true;
            int l = firePoints.Length;

            for (int i = 0; i < l; i++)
            {
                BasePoolObject bullet = _currentPool.GetObject(firePoints[i].position, firePoints[i].rotation, null);
                bullet.Activate();
            }
            
            Invoke(nameof(Reset), _currentShootInterval);
        }

        /// <summary>
        /// Reset booleans to shoot again.
        /// </summary>
        private void Reset()
        {
            _canShoot = true;
            _isShooting = false;
        }
    }
}