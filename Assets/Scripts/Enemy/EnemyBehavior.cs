using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private EnemyStats enemyStats;
    private Transform player;

    private Rigidbody2D rb;
    private Vector2 targetPos;
    private Vector2 moveDirection; // To store the move direction calculated in Update

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        enemyStats = GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomTargetPosition();
    }

    private void Update() {
        if (player != null) {
            UpdateTargetPosition();
            CalculateMoveDirection();
        }
    }

    private void FixedUpdate() {
        Move();
        ConstrainWithinBounds();
    }

    private void UpdateTargetPosition() {
        if (Vector2.Distance(transform.position, player.position) <= enemyStats.detectionRange) {
            targetPos = player.position;
        } else if (Vector2.Distance(transform.position, targetPos) <= enemyStats.stoppingDistance) {
            SetRandomTargetPosition();
        }
    }

    private void CalculateMoveDirection() {
        moveDirection = (targetPos - rb.position).normalized;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, enemyStats.rotationSpeed * Time.deltaTime);
    }

    private void Move() {
        rb.velocity = moveDirection * enemyStats.moveSpeed;
    }

    private void ConstrainWithinBounds() {
        if (!BoundaryManager.instance.IsWithinBounds(rb.position + rb.velocity * Time.fixedDeltaTime)) {
            SetRandomTargetPosition();
        }
    }

    private void SetRandomTargetPosition() {
        float randomX = Random.Range(BoundaryManager.instance.minX, BoundaryManager.instance.maxX);
        float randomY = Random.Range(BoundaryManager.instance.minY, BoundaryManager.instance.maxY);
        targetPos = new Vector2(randomX, randomY);
    }
}
