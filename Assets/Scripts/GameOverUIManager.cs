using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUIManager : MonoBehaviour
{
    public TextMeshProUGUI enemiesKilledText;
    public TextMeshProUGUI damageDealtText;
    public TextMeshProUGUI damageReceivedText;

    private void Start() {
        enemiesKilledText.SetText($"Enemies killed: {PersistentData.Instance.enemiesKilled}");
        damageDealtText.SetText($"Damage dealt: {PersistentData.Instance.damageDealt}");
        damageReceivedText.SetText($"Damage taken: {PersistentData.Instance.damageReceived}");
    }
}
