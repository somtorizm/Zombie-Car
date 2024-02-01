using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    public float carSpeed = 50f;

    public float totalDistanceInMiles = 10f;
    private float remainingDistance;
    private bool isPaused = false;
    private bool reachedDestination = false;
    private bool zombieIsDisable = false;

    [SerializeField]
    public TextMeshProUGUI distanceText;

    [SerializeField]
    public float countdownDurationInSeconds = 60f; 
    private float speed;

    [SerializeField]
    private GameObject zombieSpawner;

    [SerializeField]
    private GameObject gameOverPanel;

    private float timeScaleTarget = 0f;
    private float timeScaleSmoothness = 80f;

    void Start()
    {
        remainingDistance = totalDistanceInMiles;
        speed = totalDistanceInMiles / countdownDurationInSeconds;
    }


    private void Update()
    {
        UpdateDistanceUI();
    }

    private void UpdateDistanceUI()
    {
        if (remainingDistance > 0f)
        {
            remainingDistance -= speed * Time.deltaTime; // Update remaining distance
            distanceText.text = "Distance: " + remainingDistance.ToString("F1") + " miles";
            DisableZombieSpawn();

        }
        else
        {
            remainingDistance = 0f;
            if(!reachedDestination)
            {
                reachedDestination = true;
                TogglePause();
            }
           
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            StartCoroutine(SmoothPause());
            gameOverPanel.SetActive(true);
        }
        else
        {
            ResumeGame();
        }
    }

    IEnumerator SmoothPause() {
        float elapsedTime = 0f;

        while (Time.timeScale > timeScaleTarget)
        {
            // Use unscaledDeltaTime to avoid affecting the coroutine timing
            elapsedTime += Time.unscaledDeltaTime;

            // Calculate the interpolation factor based on the elapsed time and desired smoothness
            float t = Mathf.Clamp01(elapsedTime / timeScaleSmoothness);

            // Smoothly interpolate towards the target time scale
            Time.timeScale = Mathf.Lerp(Time.timeScale, timeScaleTarget, t);

            yield return null;
        }

        Time.timeScale = timeScaleTarget;

    // Additional pause-related functionality here (e.g., showing a pause menu)
}



    void ResumeGame()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f; // Set time scale back to 1 to resume the gam
    }

    void DisableZombieSpawn()
    {
        if(remainingDistance < 4f && !zombieIsDisable)
        {
            zombieIsDisable = true;
            zombieSpawner.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
    }

}
