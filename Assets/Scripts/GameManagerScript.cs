using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    public float carSpeed = 50f;

    public float totalDistanceInMiles = 10f;
    private float remainingDistance;

    [SerializeField]
    public TextMeshProUGUI distanceText;

    [SerializeField]
    public float countdownDurationInSeconds = 60f; 
    private float speed;

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

        }
        else
        {
            remainingDistance = 0f; // Ensure the distance doesn't go below zero
        }
    }

}
