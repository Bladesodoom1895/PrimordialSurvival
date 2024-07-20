using UnityEngine;

public class DamageCalculator : MonoBehaviour
{
    public static float CalculateFinalDamage(CharacterStats stats) {
        float finalDamage = stats.primaryAttackDamage;

        finalDamage += stats.elementalDamage;

        if (IsCriticalHit(stats.criticalChance)) {
            finalDamage *= stats.criticalDamage;
        }
        return finalDamage;
    }

    private static bool IsCriticalHit(float criticalChance) {
        return Random.value < criticalChance;
    }
}