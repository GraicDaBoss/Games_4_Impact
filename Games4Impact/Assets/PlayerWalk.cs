using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerWalk : MonoBehaviour
{
    public InputActionAsset InputActions;

    public float WalkSpeed = 5f;
    public float RotationSpeed = 12f;
    public float JumpForce = 5f;

    private InputAction _move;
    private InputAction _jump;

    private Rigidbody _rb;

    private Vector2 _moveInput;
    private bool _jumpQueued;
    private bool _grounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        _move = InputActions.FindAction("Move");
        _jump = InputActions.FindAction("Jump");
    }

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Update()
    {
        _moveInput = _move.ReadValue<Vector2>();

        if (_jump.WasPressedThisFrame())
            _jumpQueued = true;
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
        Rotate();
        Jump();
    }

    private void Move()
    {
        Transform cam = Camera.main.transform;

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir =
            forward * _moveInput.y +
            right * _moveInput.x;

        _rb.MovePosition(
            _rb.position +
            moveDir * WalkSpeed * Time.fixedDeltaTime
        );

        if (moveDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(moveDir);

            _rb.MoveRotation(
                Quaternion.Slerp(
                    _rb.rotation,
                    targetRotation,
                    RotationSpeed * Time.fixedDeltaTime
                )
            );
        }
    }

    private void Rotate()
    {
        Vector3 direction = new Vector3(_moveInput.x, 0f, _moveInput.y);

        if (direction.sqrMagnitude == 0f)
            return;

        Quaternion target = Quaternion.LookRotation(direction);

        _rb.MoveRotation(
            Quaternion.Slerp(
                _rb.rotation,
                target,
                RotationSpeed * Time.fixedDeltaTime
            )
        );
    }

    private void Jump()
    {
        if (!_jumpQueued)
            return;

        _jumpQueued = false;

        if (!_grounded)
            return;

        _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    private void CheckGround()
    {
        _grounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            0.25f
        );
    }
}