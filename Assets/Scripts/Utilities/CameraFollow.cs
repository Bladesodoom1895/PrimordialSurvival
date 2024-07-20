using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 0.125f;

    public Vector3 offset;
    private Vector2 mapSize;

    private void Start() {
        LevelGenerator levelGenerator = FindAnyObjectByType<LevelGenerator>();

        if (levelGenerator != null) {
            mapSize = levelGenerator.GetMapSize();
        }
    }

    // Update is called once per frame
    void LateUpdate() {
        if (target == null)
            return;

        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothing);

        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        float minX = camWidth;
        float maxX = mapSize.x - camWidth;
        float minY = camHeight;
        float maxY = mapSize.y - camHeight;

        smoothedPos.x = Mathf.Clamp(smoothedPos.x, minX, maxX);
        smoothedPos.y = Mathf.Clamp(smoothedPos.y, minY, maxY);

        transform.position = new Vector3(smoothedPos.x, smoothedPos.y, transform.position.z);
    }
}
