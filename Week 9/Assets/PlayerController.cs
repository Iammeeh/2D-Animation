using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public Collider2D melee_Hitbox;
    public GameObject projectilePrefab;
    public Transform spawnProjectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        melee_Hitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("MeleeAttack");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("RangeAttack");
        }
    }

    public void SpawnProjectile()
    {
        GameObject fireball = Instantiate(projectilePrefab, spawnProjectile.position, spawnProjectile.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.linearVelocity = spawnProjectile.right * 20;
    }

    public void MeleeHitbox_Enabled()
    {
        melee_Hitbox.enabled = true;
    }
    public void MeleeHitbox_Disabled()
    {
        melee_Hitbox.enabled = false;
    }
}
