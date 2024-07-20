using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float maxShield = 20f;
    public float primaryAttackRange = 1f;
    public float primaryAttackSpeed = 1f;
    public float primaryAttackDamage = 10f;
    public float secondaryAttackRange = 1f;
    public float secondaryAttackSpeed = 0.5f;
    public float secondaryAttackDamage = 25f;
    public float elementalDamage = 5f;
    public float elementalChance = 5f;
    public float criticalDamage = 20f;
    public float criticalChance = 0.1f;
    public float moveSpeed = 5f;
    public float currentHealth;
    public float currentShield;
    public int level = 1;
    public float experience = 0;
    public int experienceToLevel = 100;

    public CharacterStats stats;

    private void Start() {
        stats = FindAnyObjectByType<CharacterStats>();
        if(PersistentData.Instance != null) {
            PersistentData.Instance.LoadStats(stats);
        }
        currentHealth = maxHealth;
        currentShield = maxShield;

        UpdateUI();
    }

    public void OnDestroySave() {
        if (PersistentData.Instance != null) {
            PersistentData.Instance.SaveStats(stats);
        }
    }

    public void TakeDamage(float damage) {
        float remainingDamage = damage;
        PersistentData.Instance.AddDamageReceived(damage);
        if (currentShield > 0) {
            if (currentShield >= remainingDamage) {
                currentShield -= remainingDamage;
                remainingDamage = 0;
            }
            else {
                remainingDamage -= currentShield;
                currentShield = 0;
            }
        }

        if (remainingDamage > 0) {
            currentHealth -= remainingDamage;
            if (currentHealth <= 0) {
                Die();
            }
        }
        UpdateUI();
    }

    public void GainExperience(float xp) {
        experience += xp;
        if (experience >= experienceToLevel) {
            LevelUp();
        }
        UpdateUI();
    }

    void LevelUp() {
        level++;
        experience -= experienceToLevel;
        experienceToLevel = Mathf.CeilToInt(experienceToLevel * 1.5f);

        maxHealth += 5;
        currentHealth = maxHealth;
        maxShield += 3;
        currentShield = maxShield;
        primaryAttackDamage += 3;
        secondaryAttackDamage += 5;

        UpdateUI();
    }

    void UpdateUI() {
        if (UIManager.instance != null) {
            UIManager.instance.UpdateHealth(currentHealth, maxHealth);
            UIManager.instance.UpdateShield(currentShield, maxShield);
            UIManager.instance.UpdateLevel(level);
            UIManager.instance.UpdateExperience(experience, experienceToLevel);
        }
    }

    void Die() {
        UIManager.instance.StopTimer();
        OnDestroySave();
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene("Game Over");
    }
}
