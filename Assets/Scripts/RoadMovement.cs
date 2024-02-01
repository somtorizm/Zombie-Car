
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    private GameManagerScript gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time  * (gameManager.carSpeed / 20);
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 0);
    }
}
