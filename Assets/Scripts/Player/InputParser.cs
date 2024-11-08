using UnityEngine;
using UnityEngine.InputSystem;

using Framework;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Turner))]
    [RequireComponent(typeof(Shooting))]
    public sealed class InputParser : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private InputActionMap _playerControlsActions;
        
        private Movement _movement;
        private Turner _turner;
        private Shooting _shooting;
        
        private void Awake()
        {
            GetReferences();
            InitActionMap();
        }

        /// <summary>
        /// Reads and gives input for action that are held down.
        /// </summary>
        private void FixedUpdate()
        {
            Vector2 moveInput = _playerControlsActions["Move"].ReadValue<Vector2>();
            _movement.Move(moveInput);

            float turnInput = _playerControlsActions["Turn"].ReadValue<float>();
            _turner.Turn(turnInput);
        }

        private void OnEnable() => AddListeners();
        
        private void OnDisable() => RemoveListeners();

        /// <summary>
        /// Removes listeners, applies action map and add listeners back again.
        /// Used for switch in pause menu.
        /// </summary>
        public void SwitchActionMap()
        {
            RemoveListeners();
            InitActionMap();
            AddListeners();
        }
        
        private void GetReferences()
        {
            _playerInput = GetComponent<PlayerInput>();
            _movement = GetComponent<Movement>();
            _turner = GetComponent<Turner>();
            _shooting = GetComponent<Shooting>();
        }

        /// <summary>
        /// Will set the action map with the correct control hardware.
        /// Keyboard & mouse or controller.
        /// </summary>
        private void InitActionMap()
        {
            bool setting = PlayerSettings.Instance.IsUsingController;
            
            _playerInput.SwitchCurrentActionMap(setting ? "Controller" : "Player");
            _playerControlsActions = _playerInput.actions.actionMaps[setting ? 1 : 0];
        }

        private void AddListeners()
        {
            _playerControlsActions["Shoot"].performed += Shoot;
            _playerControlsActions["SwitchWeapon"].performed += Switch;
            _playerControlsActions["Reset"].performed += Reset;
        }
        
        private void RemoveListeners()
        {
            _playerControlsActions["Shoot"].performed -= Shoot;
            _playerControlsActions["SwitchWeapon"].performed -= Switch;
            _playerControlsActions["Reset"].performed -= Reset;
        }
        
        #region Context

        private void Shoot(InputAction.CallbackContext context) => _shooting.ActivateShoot();

        private void Switch(InputAction.CallbackContext context) => _shooting.SwitchBullet();

        private void Reset(InputAction.CallbackContext context) => _turner.ResetRotation();

        #endregion
    }
}