using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    private Animator animator;
    private Vector2 rawInput;
    private bool isAttacking;

    // Cached Animator Parameter Hashes for performance
    private static readonly int MoveXHash = Animator.StringToHash("MoveX");
    private static readonly int MoveYHash = Animator.StringToHash("MoveY");
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleAttackInput();
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        // Optional: Block movement input while attacking
        if (isAttacking)
        {
            animator.SetFloat(MoveXHash, 0f);
            animator.SetFloat(MoveYHash, 0f);
            return;
        }

        rawInput = Vector2.zero;

        // Poll Keyboard using New Input System
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) rawInput.y += 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) rawInput.y -= 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) rawInput.x -= 1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) rawInput.x += 1f;
        }

        // Poll Gamepad Left Stick
        if (Gamepad.current != null)
        {
            Vector2 stick = Gamepad.current.leftStick.ReadValue();
            if (stick.sqrMagnitude > 0.01f) rawInput = stick;
        }

        Vector2 moveDirection = rawInput.normalized;

        // Pass movement values to Blend Tree
        animator.SetFloat(MoveXHash, moveDirection.x);
        animator.SetFloat(MoveYHash, moveDirection.y);

        // Move character transform
        transform.position += (Vector3)(moveDirection * (moveSpeed * Time.deltaTime));
    }

    private void HandleAttackInput()
    {
        bool attackPressed = false;

        // Poll Space / J key or Left Mouse Button via New Input System
        if (Keyboard.current != null)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.jKey.wasPressedThisFrame)
            {
                attackPressed = true;
            }
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            attackPressed = true;
        }

        if (attackPressed && !isAttacking)
        {
            animator.SetTrigger(AttackHash);
        }
    }

    // ========================================================
    // OPTIONAL: Callbacks if using Animation Events on the Attack clip
    // ========================================================
    public void OnAttackStart()
    {
        isAttacking = true;
    }

    public void OnAttackEnd()
    {
        isAttacking = false;
    }
}