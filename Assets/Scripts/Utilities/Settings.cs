using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_Dropdown qualityDropdown;
    public Slider sensitivitySlider;
    public Button applyButton;
    public Button mainMenuButton;

    private void Start() {
        LoadSettings();

        applyButton.onClick.AddListener(ApplySettings);
        mainMenuButton.onClick.AddListener(BackToPreviousMenu);
    }

    void LoadSettings() {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.35f);
        qualityDropdown.value = PlayerPrefs.GetInt("Quality", 2);
    }

    public void ApplySettings() {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetInt("Quality", qualityDropdown.value);

        ApplyVolume(volumeSlider.value);
        ApplyQuality(qualityDropdown.value);
    }

    void ApplyVolume(float volume) {
        AudioListener.volume = volume;
    }

    void ApplyQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    void BackToPreviousMenu() {
        SceneManager.LoadScene("Main Menu");
    }
}
