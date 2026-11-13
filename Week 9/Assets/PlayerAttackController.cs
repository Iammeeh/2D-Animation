using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem; // Uses Unity's New Input System
public class PlayerCombatController : MonoBehaviour
{
    [Header("Melee Setup")]
    [Tooltip("Drag the MeleeHitbox child Collider2D here.")]
    public Collider2D meleeHitbox;
    [Header("Ranged Setup")]
    [Tooltip("Drag your Projectile Prefab from the Project folder here.")]
public GameObject projectilePrefab;
    [Tooltip("Drag the FirePoint child Transform here.")]
    public Transform firePoint;
    public float projectileSpeed = 15f;
    private Animator animator;
    // Cache Animator Parameter Hashes for clean performance
    private static readonly int MeleeHash =
    Animator.StringToHash("MeleeAttack");
    private static readonly int RangedHash =
    Animator.StringToHash("RangedAttack");
    private void Awake()
    {
        animator = GetComponent<Animator>();
        // Ensure the melee hitbox starts turned OFF when the game begins
    if (meleeHitbox != null)
        {
            meleeHitbox.enabled = false;
        }
    }
    private void Update()
    {
        if (Keyboard.current == null) return;
        // Press J for Melee Attack
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            animator.SetTrigger(MeleeHash);
            Debug.Log("<color=cyan>Melee Attack Triggered (J) </ color > ");
        }
        // Press K for Ranged Attack
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            animator.SetTrigger(RangedHash);
            Debug.Log("<color=cyan>Ranged Attack Triggered (K) </ color > ");
        }
    }
    // ========================================================
    // ANIMATION EVENT CALLBACKS (The Director's Whistles)
    // ========================================================
    // 1. Pin this to the frame where the melee weapon extends
    public void EnableMeleeHitbox()
    {
        if (meleeHitbox != null)
        {
            meleeHitbox.enabled = true;
            Debug.Log("<color=yellow>Whistle Blown: Melee Hitbox ON! </ color > ");
        }
    }
    // 2. Pin this to the frame where the melee swing finishes
    public void DisableMeleeHitbox()
    {
        if (meleeHitbox != null)
        {
            meleeHitbox.enabled = false;
            Debug.Log("<color=red>Whistle Blown: Melee Hitbox OFF! </ color > ");
        }
    }
    // 3. Pin this to the exact frame the gun flashes / bow releases
public void FireProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Spawn the bullet at the FirePoint's position androtation
            GameObject bullet = Instantiate(projectilePrefab,
            firePoint.position, firePoint.rotation);
            // Push the bullet forward
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.right * projectileSpeed;
            }
            // Automatically destroy the bullet after 3 seconds so it doesn't clutter the scene//
        Destroy(bullet, 3f);
            Debug.Log("<color=green>Whistle Blown: Projectile Fired! </ color > ");
        }
    }
}
