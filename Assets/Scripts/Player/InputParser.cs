using UnityEngine;
using UnityEngine.InputSystem;

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
        private InputActionAsset _inputActionAsset;
        
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
            Vector2 moveInput = _inputActionAsset["Move"].ReadValue<Vector2>();
            _movement.Move(moveInput);

            float turnInput = _inputActionAsset["Turn"].ReadValue<float>();
            _turner.Turn(turnInput);
        }

        private void OnEnable() => AddListeners();
        
        private void OnDisable() => RemoveListeners();
        
        private void GetReferences()
        {
            _playerInput = GetComponent<PlayerInput>();
            _movement = GetComponent<Movement>();
            _turner = GetComponent<Turner>();
            _shooting = GetComponent<Shooting>();
        }

        private void Init()
        {
            _inputActionAsset = _playerInput.actions;
        }

        private void AddListeners()
        {
            _inputActionAsset["Shoot"].performed += Shoot;
        }
        
        private void RemoveListeners()
        {
            _inputActionAsset["Shoot"].performed -= Shoot;
        }
        
        #region Context

        private void Shoot(InputAction.CallbackContext context) => _shooting.ActivateShoot();

        #endregion
    }
}