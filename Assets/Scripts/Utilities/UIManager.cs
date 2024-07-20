using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private GameObject health;
    private GameObject shield;
    private GameObject level;
    private GameObject experience;
    private GameObject enemiesRemaining;
    private GameObject timer;
    private GameObject stage;

    public float currentTimer;
    private float gameStartTime;
    private bool isTiming;
    public int currentStage = 1;

    private TextMeshProUGUI healthText;
    private TextMeshProUGUI shieldText;
    private TextMeshProUGUI levelText;
    private TextMeshProUGUI experienceText;
    private TextMeshProUGUI enemiesRemainingText;
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI stageText;

    

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            gameStartTime = Time.time;
        }
        else {
            Destroy(gameObject);
        }
        health = GameObject.Find("PlayerHealth");
        shield = GameObject.Find("PlayerShield");
        level = GameObject.Find("PlayerLevel");
        experience = GameObject.Find("PlayerExperience");
        enemiesRemaining = GameObject.Find("EnemiesLeft");
        timer = GameObject.Find("Timer");
        stage = GameObject.Find("Stage");

        healthText = health.GetComponent<TextMeshProUGUI>();
        shieldText = shield.GetComponent<TextMeshProUGUI>();
        levelText = level.GetComponent<TextMeshProUGUI>();
        experienceText = experience.GetComponent<TextMeshProUGUI>();
        enemiesRemainingText = enemiesRemaining.GetComponent<TextMeshProUGUI>();
        timerText = timer.GetComponent<TextMeshProUGUI>();
        stageText = stage.GetComponent<TextMeshProUGUI>();
        UpdateStage();
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
        if (scene.name == "Game Scene") {
            health = GameObject.Find("PlayerHealth");
            shield = GameObject.Find("PlayerShield");
            level = GameObject.Find("PlayerLevel");
            experience = GameObject.Find("PlayerExperience");
            enemiesRemaining = GameObject.Find("EnemiesLeft");
            timer = GameObject.Find("Timer");
            stage = GameObject.Find("Stage");

            healthText = health.GetComponent<TextMeshProUGUI>();
            shieldText = shield.GetComponent<TextMeshProUGUI>();
            levelText = level.GetComponent<TextMeshProUGUI>();
            experienceText = experience.GetComponent<TextMeshProUGUI>();
            enemiesRemainingText = enemiesRemaining.GetComponent<TextMeshProUGUI>();
            timerText = timer.GetComponent<TextMeshProUGUI>();
            stageText = stage.GetComponent<TextMeshProUGUI>();
            UpdateStage();
            isTiming = true;
        }
    }

    private void Start() {
        if (PersistentData.Instance != null) {
            gameStartTime = PersistentData.Instance.LoadTime();
        }
        else {
            gameStartTime = Time.time;
        }
        isTiming = true;
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void Update() {
        if (isTiming) {
            UpdateTimerText();
        }
    }

    public float GetScalingFactor() {
        float elapsedTime = Time.time - gameStartTime;
        return 1 + (elapsedTime / 60f);
    }

    public void UpdateTimerText() {
        float timer = Time.time - gameStartTime;
        int hours = Mathf.FloorToInt(timer / 3600f);
        int minutes = Mathf.FloorToInt((timer % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        string timeText = string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);
        timerText.text = "Time: " + timeText;
    }

    public void StopTimer() {
        isTiming = false;
        if (PersistentData.Instance != null) {
            PersistentData.Instance.SaveTime(currentTimer);
        }
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthText.text = "Health: " + currentHealth + " / " + maxHealth;
    }

    public void UpdateShield(float currentShield, float maxShield)
    {
        shieldText.text = "Shield: " + currentShield + " / " + maxShield;
    }

    public void UpdateLevel(int level)
    {
        levelText.text = "Level: " + level;
    }

    public void UpdateExperience(float xp, float xpToLevel) {
        experienceText.text = "Experience: " + xp + " / " + xpToLevel;
    }

    public void UpdateEnemiesRemaining(int count)
    {
        enemiesRemainingText.text = "Enemies Remaining: " + count;
    }

    public void UpdateStage() {
        stageText.text =  "Stage: " + currentStage;
    }

    public int IncreaseStage() {
        return currentStage++;
    }
}
