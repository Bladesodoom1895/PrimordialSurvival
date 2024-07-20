using UnityEngine;
using UnityEngine.UI;

public class HealthBarFollow : MonoBehaviour
{
    public Image healthBarFill;
    public Transform target;

    private void Start() {
        target = transform.parent;
    }

    private void Update() {
        transform.rotation = Quaternion.identity;
    }
    public void SetHealth(float healthPercent) {
        healthBarFill.fillAmount = healthPercent;
    }
}
