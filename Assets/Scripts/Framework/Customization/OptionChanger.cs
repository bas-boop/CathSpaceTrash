using System;
using UnityEngine;

namespace Framework.Customization
{
    public sealed class OptionChanger : MonoBehaviour
    {
        private const string ERROR_INVALID_OPTION = "That is not a valid option for this object.";
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] sprites;

        /// <summary>
        /// Change the sprite that is given as parameter.
        /// </summary>
        /// <param name="option">The selected option</param>
        /// <exception cref="Exception">When the option is not valid</exception>
        public void ChangeSprite(int option)
        {
            if (option < 0
                || option > sprites.Length)
                throw new Exception(ERROR_INVALID_OPTION);

            spriteRenderer.sprite = sprites[option];
        }
    }
}