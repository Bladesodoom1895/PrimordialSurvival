using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public string itemName;
    public float maxHealthModifier;
    public float maxShieldModifier;
    public float moveSpeedModifier;
    public float primaryAttackRangeModifier;
    public float primaryAttackSpeedModifier;
    public float primaryAttackDamageModifier;
    public float secondaryAttackRangeModifier;
    public float secondaryAttackSpeedModifier;
    public float secondaryAttackDamageModifier;
    public float elementalDamageModifier;
    public float elementalChanceModifier;
    public float criticalDamageModifier;
    public float criticalChanceModifier;

    public void ApplyEffect(CharacterStats characterStats) {
        characterStats.maxHealth += maxHealthModifier;
        characterStats.maxShield += maxShieldModifier;
        characterStats.moveSpeed += moveSpeedModifier;
        characterStats.primaryAttackRange += primaryAttackRangeModifier;
        characterStats.primaryAttackSpeed += primaryAttackSpeedModifier;
        characterStats.primaryAttackDamage += primaryAttackDamageModifier;
        characterStats.secondaryAttackRange += secondaryAttackRangeModifier;
        characterStats.secondaryAttackSpeed += secondaryAttackSpeedModifier;
        characterStats.secondaryAttackDamage += secondaryAttackDamageModifier;
        characterStats.elementalDamage += elementalDamageModifier;
        characterStats.elementalChance += elementalChanceModifier;
        characterStats.criticalDamage += criticalDamageModifier;
        characterStats.criticalChance += criticalChanceModifier;
    }
}
