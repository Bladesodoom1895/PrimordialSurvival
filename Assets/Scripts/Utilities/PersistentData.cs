using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData Instance;

    public int selectedCharacterIndex;
    public GameObject[] characterPrefabs;
    public bool isInRun = false;

    public int enemiesKilled;
    public float damageDealt;
    public float damageReceived;
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

    public float elapsedTime;
    public int currentStage = 1;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void AddEnemyKilled() {
        enemiesKilled++;
    }

    public void AddDamageDealt(float damage) {
        damageDealt += damage;
    }

    public void AddDamageReceived(float damage) {
        damageReceived += damage;
    }

    public void SaveStats(CharacterStats charStats) {
        maxHealth = charStats.maxHealth;
        maxShield = charStats.maxShield;
        primaryAttackRange = charStats.primaryAttackRange;
        primaryAttackSpeed = charStats.primaryAttackSpeed;
        primaryAttackDamage = charStats.primaryAttackDamage;
        secondaryAttackRange = charStats.secondaryAttackRange;
        secondaryAttackSpeed = charStats.secondaryAttackSpeed;
        secondaryAttackDamage = charStats.secondaryAttackDamage;
        elementalDamage = charStats.elementalDamage;
        elementalChance = charStats.elementalChance;
        criticalDamage = charStats.criticalDamage;
        criticalChance = charStats.criticalChance;
        moveSpeed = charStats.moveSpeed;
        level = charStats.level;
        experience = charStats.experience;
        experienceToLevel = charStats.experienceToLevel;
    }

    public void LoadStats(CharacterStats charStats) {
        charStats.maxHealth = maxHealth;
        charStats.maxShield = maxShield;
        charStats.moveSpeed = moveSpeed;
        charStats.primaryAttackRange = primaryAttackRange;
        charStats.primaryAttackSpeed = primaryAttackSpeed;
        charStats.primaryAttackDamage = primaryAttackDamage;
        charStats.secondaryAttackRange = secondaryAttackRange;
        charStats.secondaryAttackSpeed = secondaryAttackSpeed;
        charStats.secondaryAttackDamage = secondaryAttackDamage;
        charStats.elementalDamage = elementalDamage;
        charStats.elementalChance = elementalChance;
        charStats.criticalDamage = criticalDamage;
        charStats.criticalChance = criticalChance;
        charStats.level = level;
        charStats.experience = experience;
        charStats.experienceToLevel = experienceToLevel;
    }

    public void SaveTime(float time) {
        elapsedTime = time;
    }

    public float LoadTime() {
        return elapsedTime;
    }

    public void SaveStage(int stage) {
        currentStage = stage;
    }

    public int LoadStage() {
        return currentStage;
    }

    public void IncreaseStage() {
        currentStage++;
    }
}