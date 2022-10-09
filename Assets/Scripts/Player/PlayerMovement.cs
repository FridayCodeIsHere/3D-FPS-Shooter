using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(InputManager), typeof(PlayerLook))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 2f;

    private InputManager _inputManager;
    private PlayerLook _playerLook;
    private CharacterController _characterController;
    private Vector3 _playerVelocity;
    private Vector2 _moveDirection;

    private bool _isGrounded;
    

    private void OnValidate()
    {
        if (_speed < 0f) _speed = 0f;
        if (_jumpHeight < 0f) _jumpHeight = 0f;
    }
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerLook = GetComponent<PlayerLook>();
    }

    private void Start()
    {
        InitInputSystem();
        
    }

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;
        _moveDirection = _inputManager.PlayerAction.Movement.ReadValue<Vector2>();
        
    }

    private void FixedUpdate()
    {
        ProcessMove(_moveDirection);
        PhysicsMovement();
        
    }

    private void LateUpdate()
    {
        Vector2 input = _inputManager.PlayerAction.Look.ReadValue<Vector2>();
        _playerLook.ProcessLook(input);
    }

    private void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        _characterController.Move(transform.TransformDirection(moveDirection) * (_speed * Time.deltaTime));
    }

    private void PhysicsMovement()
    {
        _playerVelocity.y += _gravity * Time.deltaTime;

        if (_isGrounded && _playerVelocity.y < 0f)
            _playerVelocity.y = -2f;
        
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -3f * _gravity);
        }
    }

    private void InitInputSystem()
    {
        _inputManager = GetComponent<InputManager>();
        _inputManager.PlayerAction.Jump.performed += context => Jump();
    }
}
