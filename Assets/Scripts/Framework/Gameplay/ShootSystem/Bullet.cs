using UnityEngine;
using UnityEngine.Events;

using Framework.Attributes;
using Framework.ObjectPooling;

namespace Framework.Gameplay.ShootSystem
{
    [RequireComponent(typeof(BasePoolObject))]
    public sealed class Bullet : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private float speed = 1;
        [SerializeField] private float despawnDistance = 10;
        [SerializeField] private int penetration = 2;
        
        [Header("Behavior to target")]
        [SerializeField, Tag] private string targetTag;
        [SerializeField] private int damage = 1;

        [Header("Events")] 
        [SerializeField] private UnityEvent onHit = new();
        [SerializeField] private UnityEvent onDestroy = new();

        private BasePoolObject _poolObject;
        private Rigidbody2D _rigidbody2D;
        private Health _otherHealth;
        private GameObject _lastHitTarget;
        
        private Vector2 _currentDirection;
        private Vector2 _spawnPosition;

        private int _currentPenetration;
        
        private void Awake()
        {
            GetRequirements();
            Respawn();
        }

        private void OnEnable()
        { 
            _spawnPosition = transform.position;
            Move();
        }

        private void Update()
        {
            Vector3 traveledPosition = transform.position - (Vector3)_spawnPosition;

            if (traveledPosition.magnitude > despawnDistance) 
                _poolObject.ReturnToPool();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(targetTag))
                OnTargetHit(other);
        }

        /// <summary>
        /// When hitting we give that damage, and we adjust the current penetration level of this bullet.
        /// </summary>
        /// <param name="other">The object we have hit.</param>
        private void OnTargetHit(Collider2D other)
        {
            if (_lastHitTarget != other.gameObject)
                _otherHealth = other.GetComponent<Health>();

            _otherHealth.TakeDamage(damage);
            onHit?.Invoke();
            _currentPenetration--;

            if (_currentPenetration != 0)
                return;
            
            onDestroy?.Invoke();
            Destroy(gameObject);
        }

        /// <summary>
        /// Get components the Rigidbody2D
        /// </summary>>
        private void GetRequirements()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _poolObject = GetComponent<BasePoolObject>();

            _poolObject.OnReset += Respawn;
        }

        /// <summary>
        /// When respawning we reset current data to the starting values;
        /// </summary>
        private void Respawn() => _currentPenetration = penetration;

        private void Move() => _rigidbody2D.velocity = transform.up * speed;
    }
}