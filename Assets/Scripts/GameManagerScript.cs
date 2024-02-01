using UnityEngine;
using TMPro;

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
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Set time scale to 0 to pause the game
        // You can also add additional pause-related functionality here (e.g., showing a pause menu)
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Set time scale back to 1 to resume the game
        // You can also add additional resume-related functionality here
    }

    void DisableZombieSpawn()
    {
        if(remainingDistance < 4f && !zombieIsDisable)
        {
            zombieIsDisable = true;
            zombieSpawner.SetActive(false);
        }
    }

}
