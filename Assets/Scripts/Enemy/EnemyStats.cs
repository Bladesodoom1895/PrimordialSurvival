using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth = 50f;
    public float baseHealth = 50f;
    private float currentHealth;
    public float baseDamage = 10f;
    private float currentDamage;
    public float attackSpeed = 1f;
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    public float xp = 7;
    public float rotationSpeed = 4f;
    public float stoppingDistance = 0.5f;
    public bool isMelee;

    private float attackTimer;

    public GameObject player;
    public GameObject healthBarPrefab;
    private HealthBarFollow healthBar;
    private CharacterStats characterStats;

    public delegate void EnemeyDeathDelegate();
    public event EnemeyDeathDelegate OnEnemyDeath;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        characterStats = player.GetComponent<CharacterStats>();
        currentHealth = maxHealth;
        GameObject healthBarInstance = Instantiate(healthBarPrefab, transform.position + Vector3.up, Quaternion.identity, transform);
        healthBar = healthBarInstance.GetComponent<HealthBarFollow>();
        healthBar.SetHealth(1f);

        float scalingFactor = UIManager.instance.GetScalingFactor();
        currentHealth = baseHealth * scalingFactor;
        currentDamage = baseDamage * scalingFactor;
    }

    public float GetDamage() {
        return currentDamage;
    }

    public void CalculatePrimaryDamage(float damage) {
        int randCritChance = Random.Range(0, 100);
        int randElementalChance = Random.Range(0, 100);

        if (randElementalChance <= characterStats.elementalChance) {
            damage += characterStats.elementalDamage;
        }
        if (randCritChance <= characterStats.criticalChance) {
            float critAddDamage = damage + (damage / characterStats.criticalDamage);
            damage += critAddDamage;
        }
        TakeDamage(damage);
    }

    public void CalculateSecondaryDamage(float damage) {
        int randCritChance = Random.Range(0, 1000);
        int randElementalChance = Random.Range(0, 1000);

        if (randElementalChance < characterStats.elementalChance * 100) {
            damage += characterStats.elementalDamage;
        }
        if (randCritChance < characterStats.criticalChance * 100) {
            float critAddDamage = damage + (damage / characterStats.criticalDamage);
            damage += critAddDamage;
        }
        TakeDamage(damage);
    }
    public void TakeDamage(float damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            Die();
        }
        else {
            healthBar.SetHealth(currentHealth / maxHealth);
        }
    }

    void Die() {
        OnEnemyDeath?.Invoke();
        CharacterStats character = FindAnyObjectByType<CharacterStats>();
        if (character != null) {
            character.GainExperience(xp);
        }

        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        enemySpawner.HandleEnemyDeath();
        PersistentData.Instance.AddEnemyKilled();
        PersistentData.Instance.AddDamageDealt(maxHealth);

        Destroy(healthBar);
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (!isMelee) {
            return;
        }
        if (collision.gameObject.CompareTag("Player")) {
            attackTimer += Time.deltaTime;
            CharacterStats playerStats = collision.gameObject.GetComponent<CharacterStats>();

            if (playerStats != null && attackTimer >= attackSpeed) {
                playerStats.TakeDamage(baseDamage);
                attackTimer = 0;
            }
        }
    }
}
