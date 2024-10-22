using UnityEngine;
using UnityEngine.Events;

using Framework.Attributes;
using Framework.Extensions;
using Framework.Gameplay;

namespace Environment.Spawning
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BaseFallingObject : MonoBehaviour
    {
        [Header("Tags")]
        [SerializeField, Tag] private string wallTag;
        [SerializeField, Tag] private string shipTag;
        [SerializeField, Tag] private string playerTag;
        [Header("Settings")]
        [SerializeField] private Vector2 speed = Vector2.one;
        [SerializeField] private Vector2 fallDirection = Vector2.down;

        private Rigidbody2D _rigidbody2D;
        private Spawner _spawner;
        private Score _scoreSystem;

        private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

        private void Start() => _rigidbody2D.velocity = fallDirection * speed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(wallTag))
                HandelCollisionWall();
            else if (other.CompareTag(shipTag))
                HandelCollisionShip();
            else if (other.CompareTag(playerTag))
                HandelCollisionPlayer(other);
        }

        private void OnDestroy() => _spawner.Check();

        public void Setup(Spawner sender, Score score, UnityAction target)
        {
            _spawner = sender;
            _scoreSystem = score;
            GetComponent<Health>().AddListenerToDieEvent(target);
        }

        private void HandelCollisionWall()
        {
            Vector2 newDirection = _rigidbody2D.velocity;
            newDirection.InvertX();
            _rigidbody2D.velocity = newDirection;
        }

        private void HandelCollisionShip()
        {
            _scoreSystem.DecreaseScore();
            Destroy(gameObject);
        }

        private void HandelCollisionPlayer(Collider2D other)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}