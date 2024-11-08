using UnityEngine;
using UnityEngine.Events;

using Framework.Attributes;
using Framework.Extensions;
using Framework.Gameplay;

namespace Environment.Spawning
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FallingObject : MonoBehaviour
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

        /// <summary>
        /// Assigns systems and gives the die event a function to listen to.
        /// </summary>
        /// <param name="sender">The spawner we are spawning form.</param>
        /// <param name="score">The score system.</param>
        /// <param name="target">Function to add to die event.</param>
        public void Setup(Spawner sender, Score score, UnityAction target)
        {
            _spawner = sender;
            _scoreSystem = score;
            GetComponent<Health>().AddListenerToDieEvent(target);
        }

        /// <summary>
        /// Handels collision with the walls, should invert the horizontal speed.
        /// </summary>
        private void HandelCollisionWall()
        {
            Vector2 newDirection = _rigidbody2D.velocity;
            newDirection.InvertX();
            _rigidbody2D.velocity = newDirection;
        }

        /// <summary>
        /// Handels collision with mother ship, should decrease score and destroy itself.
        /// </summary>
        private void HandelCollisionShip()
        {
            _scoreSystem.DecreaseScore();
            Destroy(gameObject);
        }

        /// <summary>
        /// Handels collision with player, should destroy both.
        /// </summary>
        private void HandelCollisionPlayer(Collider2D other)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}