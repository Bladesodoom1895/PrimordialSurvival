using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public GameObject primaryPrefab;
    public GameObject secondaryPrefab;

    private float primaryCooldown = 1f;
    private float secondaryCooldown = 1f;

    public static InputManager instance;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private CharacterStats characterStats;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        characterStats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        HandleMovement();
        HandleRotation();

        if (Input.GetMouseButton(0) && Time.time >= primaryCooldown) {
            PrimaryAttack();
            primaryCooldown = Time.time + 1f / characterStats.primaryAttackSpeed;
        }

        if (Input.GetMouseButton(1) && Time.time >= secondaryCooldown) {
            SecondaryAttack();
            secondaryCooldown = Time.time + 1f / characterStats.secondaryAttackSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0;
            UIManager.instance.StopTimer();
            SceneManager.LoadScene("Settings");
        }
    }

    void HandleMovement() {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        Vector2 newPos = rb.position + moveSpeed * Time.fixedDeltaTime * movement;
        newPos.x = Mathf.Clamp(newPos.x, BoundaryManager.instance.minX, BoundaryManager.instance.maxX);
        newPos.y = Mathf.Clamp(newPos.y, BoundaryManager.instance.minY, BoundaryManager.instance.maxY);
        rb.MovePosition(newPos);
    }

    void HandleRotation() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 6f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    void PrimaryAttack() {
        GameObject projInstance = Instantiate(primaryPrefab, transform.position, Quaternion.identity);
        PrimaryScript primaryScript = projInstance.GetComponent<PrimaryScript>();
        if (primaryScript != null) {
            primaryScript.SetDamage(DamageCalculator.CalculateFinalDamage(characterStats));
        }
        primaryCooldown = Time.time + 1f / characterStats.primaryAttackSpeed;
    }

    void SecondaryAttack() {
        GameObject projInstance = Instantiate(secondaryPrefab, transform.position, Quaternion.identity);
        SecondaryScript secondaryScript = projInstance.GetComponent<SecondaryScript>();
        if (secondaryScript != null) {
            secondaryScript.SetDamage(DamageCalculator.CalculateFinalDamage(characterStats));
        }
        secondaryCooldown = Time.time + 1f / characterStats.secondaryAttackSpeed;
    }
}
