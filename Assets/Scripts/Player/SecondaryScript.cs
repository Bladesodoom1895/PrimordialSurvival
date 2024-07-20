using UnityEngine;

public class SecondaryScript : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 20f;
    private float attackRange;

    private Vector3 mousePos;
    private Vector3 startPos;
    private Rigidbody2D rb;
    public Vector2 direction;

    public CharacterStats characterStats;

    private void Start() {
        characterStats = FindAnyObjectByType<CharacterStats>();
        attackRange = characterStats.secondaryAttackRange;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        rb.velocity = direction * speed;
        Destroy(gameObject, attackRange);
    }

    private void Update() {if (Vector3.Distance(startPos, transform.position) > attackRange) {
            Destroy(gameObject);
        }
    }
    public void SetDamage(float damage) {
        this.damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
        if (enemy != null) {
            enemy.CalculateSecondaryDamage((float)damage);
        }
        Destroy(gameObject);
        
    }
}
