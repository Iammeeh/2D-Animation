using System.Diagnostics.Metrics;
using System.Drawing;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
public class EnemyDamageHandler : MonoBehaviour
{
    private Animator animator;
    // Cache the TakeDamage trigger hash
    private static readonly int DamageHash =
    Animator.StringToHash("TakeDamage");
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Automatically fires when a Trigger Collider touches the Enemy
private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object hitting us is tagged as a PlayerAttack(Melee Hitbox or Bullet)
if (collision.CompareTag("PlayerAttack"))
        {
            TakeDamage();
            // If it was a projectile (has a Rigidbody2D), destroy the bullet on impact
if (collision.attachedRigidbody != null)
            {
                Destroy(collision.gameObject);
            }
        }
    }
    private void TakeDamage()
    {
        animator.SetTrigger(DamageHash);
        Debug.Log("<color=orange>Enemy Hit! Playing Hurt Animation.</ color > ");
    }
}
