using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {

    public PersistentData persistentData;
    public Button continueButton;

    private void Start() {
        persistentData = FindAnyObjectByType<PersistentData>();
    }
    public void PlayGame() {
        SceneManager.LoadScene("Character Selection");
    }

    public void StartRun() {
        SceneManager.LoadScene("Game Scene");
    }

    public void Settings() {
        SceneManager.LoadScene("Settings");
    }

    public void Credits() {
        SceneManager.LoadScene("Credits");
    }

    public void MainMenu() {
        SceneManager.LoadScene("Main Menu");
    }
    public void ExitGame() {
        Application.Quit();
    }

    public void ContinueGame() {
        // check if player is in run
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
        // otherwise button is disabled/inactive
    }
}
