using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;

    public int xMin = 20;
    public int yMin = 20;
    public int xMax = 50;
    public int yMax = 50;

    public float noiseScale = 20f;
    public float tileSize = 1f;

    public Tilemap tilemap;
    public Tile waterTile;
    public Tile grassTile;
    public Tile mountainTile;

    public GameObject[] itemPrefabs;
    public GameObject nextLevelPortalPrefab;
    public GameObject endRunPortalPrefab;

    private int mapWidth;
    private int mapHeight;
    private float offsetX;
    private float offsetY;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    private void Start() {
        mapWidth = Random.Range(xMin, xMax);
        mapHeight = Random.Range(yMin, yMax);
        offsetX = Random.Range(0f, 100f);
        offsetY = Random.Range(0f, 100f);
        GenerateMap();
        BoundaryManager.instance.SetBoundaries(mapWidth, mapHeight, tileSize);
        PlaceItems(mapWidth, mapHeight);
        SpawnPortals();
    }

    public void GenerateMap() {
        ClearMap();

        float[,] noiseMap = GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                float currentHeight = noiseMap[x, y];
                if (currentHeight < 0.4f) {
                    tilemap.SetTile(new Vector3Int(x, y, 0), waterTile);
                }
                else if (currentHeight < 0.7f) {
                    tilemap.SetTile(new Vector3Int(x, y, 0), grassTile);
                }
                else {
                    tilemap.SetTile(new Vector3Int(x, y, 0), mountainTile);
                }
            }
        }
    }

    void ClearMap() {
        tilemap.ClearAllTiles();
    }

    void SpawnPortals() {
        Vector2 randomPos = new Vector2(
            Random.Range(BoundaryManager.instance.minX, BoundaryManager.instance.maxX),
            Random.Range(BoundaryManager.instance.minY, BoundaryManager.instance.maxY));

        Instantiate(nextLevelPortalPrefab, randomPos, Quaternion.identity);

        if (PersistentData.Instance.LoadStage() % 3 == 0) {
            randomPos = new Vector2(
                Random.Range(BoundaryManager.instance.minX, BoundaryManager.instance.maxX),
                Random.Range(BoundaryManager.instance.minY, BoundaryManager.instance.maxY));
            Instantiate(endRunPortalPrefab, randomPos, Quaternion.identity);
        }
    }

    void PlaceItems(int width, int height) {
        int numberOfItems = Random.Range(1, 30);

        for (int i = 0; i < numberOfItems; i++) {
            GameObject itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            float itemSize = itemPrefab.GetComponent<Renderer>().bounds.size.x;
            float randomX = Random.Range(itemSize / 2, width - itemSize / 2);
            float randomY = Random.Range(itemSize / 2, height - itemSize / 2);

            Vector3 itemPos = new Vector3(randomX, randomY, 1);
            
            Instantiate(itemPrefab, itemPos, Quaternion.identity);
        }
    }

    float[,] GenerateNoiseMap(int width, int height, float scale) {
        float[,] noiseMap = new float[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                float sampleX = (x + offsetX) / scale;
                float sampleY = (y + offsetY) / scale;
                float noiseValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = noiseValue;
            }
        }
        return noiseMap;
    }

    public Vector2 GetMapSize() {
        return new Vector2(mapWidth, mapHeight);
    }
}
