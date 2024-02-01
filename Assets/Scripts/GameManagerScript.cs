using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    public float carSpeed = 50f;

    [SerializeField]
    public GameObject[] gameObjects;

    [SerializeField]
    public GameObject[] roads;

    [SerializeField]
    public GameObject spawner;

    [SerializeField]
    public float totalDistanceInMiles = 10f;
    private float remainingDistance;

    [SerializeField]
    public float countdownDurationInSeconds = 60f; 
    private float speed;

    [SerializeField]
    public TextMeshProUGUI distanceText;


    void Start()
    {
        remainingDistance = totalDistanceInMiles;
        speed = totalDistanceInMiles / countdownDurationInSeconds;
    }

    private void Update()
    {
        if (remainingDistance > 0f)
        {
            remainingDistance -= speed * Time.deltaTime; // Update remaining distance
            UpdateUI();
        }
        else
        {
            remainingDistance = 0f; // Ensure the distance doesn't go below zero
        }
    }

    private void UpdateUI()
    {
        distanceText.text = "Distance: " + remainingDistance.ToString("F2") + " miles";
    }


    public void updateZombieKilled(float number)
    {
        carSpeed -= number * Time.deltaTime;
        if (carSpeed < 0)
        {
            DisableGameItems();
        }
    }

    private void DisableGameItems()
    {
        foreach (var items in roads)
        {
            items.GetComponent<RoadMovement>().enabled = false;
        }

        spawner.SetActive(false);
 
    }


}
