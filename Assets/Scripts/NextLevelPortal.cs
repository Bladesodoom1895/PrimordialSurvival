using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPortal : MonoBehaviour
{
    public CharacterStats stats;

    private void Start() {
        stats = FindAnyObjectByType<CharacterStats>();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PersistentData.Instance.SaveStats(stats);
            UIManager.instance.StopTimer();
            PersistentData.Instance.IncreaseStage();
            SceneManager.LoadScene("Game Scene");
            UIManager.instance.IncreaseStage();
            UIManager.instance.UpdateStage();
            PersistentData.Instance.LoadTime();
            UIManager.instance.UpdateTimerText();
            Destroy(gameObject);
        }
    }
}

public class EndRunPortal : MonoBehaviour {

    public CharacterStats stats;

    private void Start() {
        stats = FindAnyObjectByType<CharacterStats>();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            UIManager.instance.StopTimer();
            PersistentData.Instance.SaveStats(stats);
            SceneManager.LoadScene("Game Over");
            Destroy(gameObject);
        }
    }
}
