using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float attackCooldown = 2f;
    public float projectileSpeed = 5f;
    public float projectileLifetime = 2f;

    private float nextAttackTime = 0f;

    private readonly Vector2[] directions = {
        new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1),  // Right, Left, Up, Down
        new Vector2(1, 1).normalized, new Vector2(-1, 1).normalized, new Vector2(1, -1).normalized, new Vector2(-1, -1).normalized  // Diagonals
    };

    private void Update() {
        if (Time.time >= nextAttackTime) {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void Attack() {
        foreach (var direction in directions) {
            ShootProjectile(direction);
        }
    }

    private void ShootProjectile(Vector2 direction) {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.velocity = direction * projectileSpeed;
        }
        Destroy(projectile, projectileLifetime);
    }
}
