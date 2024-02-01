
using UnityEngine;

public class CarMovement : MonoBehaviour

{
    public float horizontalSpeed = 2f;
    public float horizontalAcceleration = 5f;  // Add acceleration for smoother movement
    public float horizontalDeceleration = 2f;  // Add deceleration for smoother stopping
    public float horizontalSensitivity = 2f;
    public float xMinLimit = -5f;
    public float xMaxLimit = 5f;

    private float currentHorizontalVelocity = 0f;  // Store current velocity

    private ButtonInteraction leftButton;
    private ButtonInteraction rightButton;

    private GameManagerScript gameManager;
    private float speed;

    [SerializeField]
    private Vector3 spawnerOffset;

    [SerializeField]
    private GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        leftButton = GameObject.FindGameObjectWithTag("LeftButton").GetComponent<ButtonInteraction>();
        rightButton = GameObject.FindGameObjectWithTag("RightButton").GetComponent<ButtonInteraction>();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        speed = gameManager.carSpeed;
    }

    void Update()
    {


        if (rightButton.isButtonDown)
        {
            currentHorizontalVelocity -= horizontalAcceleration * Time.deltaTime;
        } else if(leftButton.isButtonDown)
        {
            currentHorizontalVelocity += horizontalAcceleration * Time.deltaTime;
        }
        else
        {
            currentHorizontalVelocity = 0f;
        }

        float horizontalMovement = currentHorizontalVelocity * Time.deltaTime;
        float newXPosition = Mathf.Clamp(transform.position.x - horizontalMovement, xMinLimit, xMaxLimit);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        transform.position = transform.position + (Vector3.forward * speed) * Time.deltaTime;
        spawner.transform.position = transform.position + spawnerOffset;

    }
}
