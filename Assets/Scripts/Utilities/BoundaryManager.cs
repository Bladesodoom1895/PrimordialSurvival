using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public static BoundaryManager instance { get; private set; }

    public float minX, maxX, minY, maxY;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void SetBoundaries(int madWidth, int mapHeight, float tilesize) {
        minX = 0;
        maxX = madWidth * tilesize;
        minY = 0;
        maxY = mapHeight * tilesize;
    }

    public bool IsWithinBounds(Vector2 position) {
        return position.x >= minX && position.x <= maxX && position.y >= minY && position.y <= maxY;
    }
}
