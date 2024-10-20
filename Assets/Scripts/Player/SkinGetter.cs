using UnityEngine;

using Framework.Customization;

namespace Player
{
    public sealed class SkinGetter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] flames;

        private void Start()
        {
            Sprite target = SkinManager.Instance.GetFlame();

            foreach (SpriteRenderer flame in flames)
            {
                // if no flame was choices, select a random one
                if (target == null)
                {
                    flame.sprite = flames[Random.Range(0, flames.Length)].sprite;
                    continue;
                }
                
                flame.sprite = target;
            }
        }
    }
}