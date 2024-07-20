using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    public TMP_Dropdown characterDropdown;
    public GameObject[] characterPrefabs;
    public Image characterImage;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI characterDescriptionText;
    public Button startButton;
    public Button mainMenuButton;

    private int selectedCharacterIndex = 0;
    private Character[] characters;

    [Header("Sprites")]
    public Sprite meleeBacteria;
    public Sprite rangedBacteria;
    public Sprite meleeVirus;
    public Sprite rangedVirus;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI primaryAttackRangeText;
    public TextMeshProUGUI primaryAttackSpeedText;
    public TextMeshProUGUI primaryAttackDamageText;
    public TextMeshProUGUI secondaryAttackRangeText;
    public TextMeshProUGUI secondaryAttackSpeedText;
    public TextMeshProUGUI secondaryAttackDamageText;
    public TextMeshProUGUI elementalDamageText;
    public TextMeshProUGUI elementalChanceText;
    public TextMeshProUGUI criticalDamageText;
    public TextMeshProUGUI criticalChanceText;

    private void Start() {
        characters = new Character[] {
            new Character("Bacteria 1", "Melee bacteria", meleeBacteria),
            new Character("Bacteria 2", "Ranged bacteria", rangedBacteria),
            new Character("Virus 1", "Melee virus", meleeVirus),
            new Character("Virus 2", "Ranged virus", rangedVirus),
        };

        InitializeDropdown();
        characterDropdown.onValueChanged.AddListener(delegate { UpdateCharacterDisplay();});

        characterDropdown.onValueChanged.AddListener(delegate {
            CharacterDropdownValueChanged(characterDropdown);
        });

        PersistentData.Instance.characterPrefabs = characterPrefabs;
        UpdateCharacterStats();
        UpdateCharacterDisplay();
    }

    void InitializeDropdown() {
        if (characterDropdown.options.Count > 0) {
            characterDropdown.ClearOptions();
        }

        foreach (Character character in characters) {
            characterDropdown.options.Add(new TMP_Dropdown.OptionData(character.name));
        }
        characterDropdown.value = 0;
        characterDropdown.RefreshShownValue();
    }

    void CharacterDropdownValueChanged(TMP_Dropdown dropdown) {
        selectedCharacterIndex = dropdown.value;
        UpdateCharacterStats();
    }

    public GameObject GetSelectedCharacterPrefab() {
        return characterPrefabs[selectedCharacterIndex];
    }

    public void OnStartRunPressed() {
        PersistentData.Instance.selectedCharacterIndex = selectedCharacterIndex;
        PersistentData.Instance.isInRun = true;
        SceneManager.LoadScene("Game Scene");
    }

    void UpdateCharacterDisplay() {
        int index = characterDropdown.value;
        characterNameText.text = characters[index].name;
        characterDescriptionText.text = characters[index].description;
        characterImage.sprite = characters[index].sprite;
    }

    public void MainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    void UpdateCharacterStats() {
        int selectedCharacterIndex = characterDropdown.value;
        GameObject selectedCharacter = characterPrefabs[selectedCharacterIndex];
        CharacterStats characterStats = selectedCharacter.GetComponent<CharacterStats>();

        if (characterStats != null) {
            healthText.text = "Health: " + characterStats.maxHealth;
            shieldText.text = "Shield: " + characterStats.maxShield;
            moveSpeedText.text = "Move Speed: " + characterStats.moveSpeed;
            primaryAttackRangeText.text = "Primary Attack Range: " + characterStats.primaryAttackRange;
            primaryAttackSpeedText.text = "Primary Attack Speed: " + characterStats.primaryAttackSpeed;
            primaryAttackDamageText.text = "Primary Attack Damage: " + characterStats.primaryAttackDamage;
            secondaryAttackRangeText.text = "Secondary Attack Range: " + characterStats.secondaryAttackRange;
            secondaryAttackSpeedText.text = "Secondary Attack Speed: " + characterStats.secondaryAttackSpeed;
            secondaryAttackDamageText.text = "Secondary Attack Damage: " + characterStats.secondaryAttackDamage;
            elementalDamageText.text = "Elemental Damage: " + characterStats.elementalDamage;
            elementalChanceText.text = "Elemental Chance: " + characterStats.elementalChance;
            criticalDamageText.text = "Critical Damage: " + characterStats.criticalDamage;
            criticalChanceText.text = "Critical Chance: " + characterStats.criticalChance;
        }
        PersistentData.Instance.SaveStats(characterStats);
    }
}

[System.Serializable]
public class Character {
    public string name;
    public string description;
    public Sprite sprite;

    public Character(string name, string description, Sprite sprite) {
        this.name = name;
        this.description = description;
        this.sprite = sprite;
    }
}
