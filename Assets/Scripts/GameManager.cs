using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float survivalTime = 30f;
    public Text timerText;

    [Header("UI Screens (Canvas-based Images)")]
    public GameObject winScreenUI;     
    public GameObject loseScreenUI;     

    public GameObject spaceshipPrefab;
    public GameObject retryButton;
    public Transform spaceshipSpawnPoint;

    private float timer;
    private bool isGameOver = false;

    void Start()
    {
        timer = survivalTime;
        winScreenUI.SetActive(false);
        loseScreenUI.SetActive(false);
        retryButton.SetActive(false);

        SpawnShip();
    }

    void Update()
    {
        if (isGameOver) return;

        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timer).ToString();

        if (timer <= 0f)
        {
            timer = 0f;
            Win();
        }
    }

    void SpawnShip()
    {
        GameObject newShip = Instantiate(spaceshipPrefab, spaceshipSpawnPoint.position, Quaternion.identity);

        CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>();
        if (camFollow != null)
        {
            camFollow.target = newShip.transform;
        }
    }

    void Win()
    {
        isGameOver = true;
        winScreenUI.SetActive(true);
        retryButton.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        loseScreenUI.SetActive(true);
        retryButton.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        timer = survivalTime;
        isGameOver = false;
        winScreenUI.SetActive(false);
        loseScreenUI.SetActive(false);
        retryButton.SetActive(false);

        // Stop and restart BGM
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM();

        // Destroy all existing asteroids
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            Destroy(asteroid);
        }

        // Spawn new ship
        SpawnShip();

        // Reset timer display
        timerText.text = "Time: " + Mathf.Ceil(timer).ToString();
    }
}
