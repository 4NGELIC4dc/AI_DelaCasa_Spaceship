using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRate = 1.5f;
    public float spawnYOffset = 5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnAsteroid();
            timer = 0f;
        }
    }

    void SpawnAsteroid()
    {
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        float spawnY = Random.Range(-cameraHeight + 1f, cameraHeight - 1f);

        // Make sure it spawns always ahead of the camera
        float spawnX = Camera.main.transform.position.x + cameraWidth + 2f;

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);
        Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
    }
}
