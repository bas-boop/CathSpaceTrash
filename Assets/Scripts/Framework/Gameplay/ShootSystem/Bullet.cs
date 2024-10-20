using UnityEngine;
using UnityEngine.Events;

using Framework.Attributes;

namespace Framework.Gameplay.ShootSystem
{
    public class Bullet : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private float speed = 1;
        [SerializeField] private float penetration = 2;
        [SerializeField] private float despawnDistance = 10;
        
        [Header("Behavior to target")]
        [SerializeField, Tag] private string targetTag;
        [SerializeField] private int damage = 1;

        [Header("Events")]
        [SerializeField] private UnityEvent onHit = new();
        [SerializeField] private UnityEvent onDestroy = new();

        private Rigidbody2D _rigidbody2D;
        private Health _otherHealth;
        private GameObject _lastHitTarget;
        
        private Vector2 _currentDirection;
        private Vector2 _spawnPosition;
		
        private void Awake() => GetRequirements();

        private void OnEnable()
        { 
            _spawnPosition = transform.position;
            Move();
        }

        private void Update()
        {
            Vector3 traveledPosition = transform.position - (Vector3)_spawnPosition;
            
            if (traveledPosition.magnitude > despawnDistance)
                Destroy(gameObject);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(targetTag))
                return;

            if (_lastHitTarget != other.gameObject)
                _otherHealth = other.GetComponent<Health>();

            _otherHealth.TakeDamage(damage);
            onHit?.Invoke();
            penetration--;

            if (penetration != 0)
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
        }

        private void Move() => _rigidbody2D.velocity = transform.up * speed;
    }
}