using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private readonly int minEnemies = 5;
    private int maxEnemies;
    private Vector2 mapSize;

    private UIManager uiManager;

    private int enemeiesRemaining;
    private void Start() {

        LevelGenerator levelGenerator = FindAnyObjectByType<LevelGenerator>();
        uiManager = FindAnyObjectByType<UIManager>();

        if (levelGenerator != null) {
            mapSize = levelGenerator.GetMapSize();
            maxEnemies = Mathf.Max(minEnemies, Mathf.FloorToInt(Mathf.Sqrt(mapSize.x * mapSize.y)));
        }

        SpawnEnemies();
        UpdateEnemiesRemainingText();
    }

    void SpawnEnemies() {
        int enemyCount = Random.Range(minEnemies, maxEnemies + 1);
        enemeiesRemaining = enemyCount;

        for (int i = 0; i < enemyCount; i++) {
            Vector3 spawnPos = new Vector3(
                Random.Range(0, mapSize.x),
                Random.Range(0, mapSize.y),
                0
            );
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyInstance = Instantiate(enemyPrefabs[randomIndex], spawnPos, Quaternion.identity);
            EnemyStats enemyStats = enemyInstance.GetComponent<EnemyStats>();
            if (enemyStats != null) {
                float scalingFactor = UIManager.instance.GetScalingFactor();
                enemyStats.baseHealth *= scalingFactor;
                enemyStats.baseDamage *= scalingFactor;
            }
        }
    }

    public void HandleEnemyDeath() {
        enemeiesRemaining--;
        UpdateEnemiesRemainingText();
    }

    void UpdateEnemiesRemainingText() {
        if (uiManager != null) {
            uiManager.UpdateEnemiesRemaining(enemeiesRemaining);
        }
    }
}
