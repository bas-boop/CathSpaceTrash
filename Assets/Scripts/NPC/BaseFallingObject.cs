using UnityEngine;

using Framework.Attributes;
using Framework.Extensions;
using Framework.Gameplay;

namespace NPC
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BaseFallingObject : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Score scoreSystem;
        [Header("Tags")]
        [SerializeField, Tag] private string wallTag;
        [SerializeField, Tag] private string shipTag;
        [SerializeField, Tag] private string playerTag;
        [Header("Settings")]
        [SerializeField] private Vector2 speed = Vector2.one;
        [SerializeField] private Vector2 fallDirection = Vector2.down;
        
        private Rigidbody2D _rigidbody2D;

        private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

        private void Start() => _rigidbody2D.velocity = fallDirection * speed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(wallTag))
            {
                Vector2 newDirection = _rigidbody2D.velocity;
                newDirection.InvertX();
                _rigidbody2D.velocity = newDirection;
            }
            else if (other.CompareTag(shipTag))
            {
                scoreSystem.DecreaseScore();
                Destroy(gameObject);
            }
            else if (other.CompareTag(playerTag))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
    }
}