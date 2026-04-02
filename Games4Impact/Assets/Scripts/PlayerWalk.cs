using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerWalk : MonoBehaviour
{

    public InputActionAsset InputActions;
    public float WalkSpeed = 5f;
    public float RotationSpeed = 12f;
    public float JumpForce = 7f;
    public float GroundCheckDistance = 0.15f;

    [SerializeField] private On_Interact interact_Script;
    public bool in_Dialogue = false; // For restricting player movement when in dialogue 
    
    public GameObject audiowalk;

    private InputAction _move;
    private InputAction _jump;
    private InputAction _interact;
    private Rigidbody _rb;
    private BoxCollider _col;
    private Vector2 _moveInput;
    private bool _jumpQueued;
    private bool _grounded;
    

    [Header("Animation")]
    [SerializeField]
    private Animator _animator;

    [HideInInspector] public Vector3 ExternalVelocity; 

    private void Move()
    {
        Vector3 velocity = _rb.linearVelocity;
        velocity.x = (_moveInput.x * WalkSpeed) + ExternalVelocity.x;
        velocity.z = (_moveInput.y * WalkSpeed) + ExternalVelocity.z;
        _rb.linearVelocity = velocity;
        
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<BoxCollider>();
        _rb.freezeRotation = true;
        _move = InputActions.FindAction("Move");
        _jump = InputActions.FindAction("Jump");
        _interact = InputActions.FindAction("Interact");
        audiowalk.SetActive(false);
    }

    private void OnEnable() => InputActions.FindActionMap("Player").Enable();
    private void OnDisable() => InputActions.FindActionMap("Player").Disable();

    private void Update()
    {
        if (_interact.WasPressedThisFrame())
            trigger_Interact();

        if (in_Dialogue == true)
            return;
        
        _moveInput = _move.ReadValue<Vector2>();

        if (_moveInput.magnitude > 0)
        {
            _animator.SetBool("is_Moving", true);
        }
        else
            _animator.SetBool("is_Moving", false);
        
        if (_jump.WasPressedThisFrame())
            _jumpQueued = true;
        

    }

    private void FixedUpdate()
    {
        if (in_Dialogue == true)
            return;
        
        CheckGround();
        Move();
        Rotate();
        Jump();
    }

 

    private void Rotate()
    {
        Vector3 direction = new Vector3(_moveInput.x, 0f, _moveInput.y);
        if (direction.sqrMagnitude == 0f) return;
        Quaternion target = Quaternion.LookRotation(direction);
        _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, target,
            RotationSpeed * Time.fixedDeltaTime));
    }

    private void Jump()
    {
        if (!_jumpQueued) return;
        _jumpQueued = false; 
        
        if (!_grounded) return;
        

        Vector3 velocity = _rb.linearVelocity;
        velocity.y = 0f;
        _rb.linearVelocity = velocity;
        _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    private void CheckGround()
    {
        
        float castOriginY = _col.bounds.min.y + 0.05f;
        Vector3 origin = new Vector3(transform.position.x, castOriginY, transform.position.z);

        _grounded = Physics.Raycast(origin, Vector3.down,
            GroundCheckDistance, ~LayerMask.GetMask("Player"));
    }

    private void trigger_Interact()
    {
        interact_Script.Interact();
    }
    
}