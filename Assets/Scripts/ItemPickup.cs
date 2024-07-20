using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            CharacterStats characterStats = collision.collider.GetComponent<CharacterStats>();
            if (characterStats != null) {
                item.ApplyEffect(characterStats);
                Destroy(gameObject);
            }
        }
    }
}
