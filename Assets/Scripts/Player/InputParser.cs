using UnityEngine;
using UnityEngine.InputSystem;

using Framework;

namespace Player
{
    /// <summary>
    /// Make comments
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Turner))]
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
            Init();
        }

        private void FixedUpdate()
        {
            Vector2 moveInput = _playerControlsActions["Move"].ReadValue<Vector2>();
            _movement.Move(moveInput);

            float turnInput = _playerControlsActions["Turn"].ReadValue<float>();
            _turner.Turn(turnInput);
        }

        private void OnEnable() => AddListeners();
        
        private void OnDisable() => RemoveListeners();

        public void SwitchActionMap()
        {
            RemoveListeners();
            Init();
            AddListeners();
        }
        
        private void GetReferences()
        {
            _playerInput = GetComponent<PlayerInput>();
            _movement = GetComponent<Movement>();
            _turner = GetComponent<Turner>();
            _shooting = GetComponent<Shooting>();
        }

        private void Init()
        {
            bool setting = PlayerSettings.Instance.IsUsingController;
            
            _playerInput.SwitchCurrentActionMap(setting ? "Controller" : "Player");
            _playerControlsActions = _playerInput.actions.actionMaps[setting ? 1 : 0];
        }

        private void AddListeners()
        {
            _playerControlsActions["Shoot"].performed += Shoot;
            _playerControlsActions["Reset"].performed += Reset;
        }
        
        private void RemoveListeners()
        {
            _playerControlsActions["Shoot"].performed -= Shoot;
            _playerControlsActions["Reset"].performed -= Reset;
        }
        
        #region Context

        private void Shoot(InputAction.CallbackContext context) => _shooting.ActivateShoot();

        private void Reset(InputAction.CallbackContext context) => _turner.ResetRotation();

        #endregion
    }
}