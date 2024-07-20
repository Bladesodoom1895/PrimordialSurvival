using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public Camera mainCamera;

    private GameObject playerInstance;
    public UIManager uiManager;

    private void Start() {
        if (playerInstance != null) {
            playerInstance = GameObject.FindGameObjectWithTag("Player");
        }
        else {
            InstantiatePlayer();
        }
    }

    void InstantiatePlayer() {
        int selectedCharacterIndex = PersistentData.Instance.selectedCharacterIndex;
        GameObject[] characterPrefabs =  PersistentData.Instance.characterPrefabs;
        GameObject selectedCharacterPrefab = characterPrefabs[selectedCharacterIndex];
        playerInstance = Instantiate(selectedCharacterPrefab, playerSpawnPoint.position, Quaternion.identity);

        CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
        if (cameraFollow != null) {
            cameraFollow.target = playerInstance.transform;
        }
        
    }
}
