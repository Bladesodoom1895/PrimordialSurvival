using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 10f;

    private void OnCollisionEnter2D(Collision2D collision) {
        CharacterStats player = collision.gameObject.GetComponent<CharacterStats>();
        if (player != null) {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        
    }
}
