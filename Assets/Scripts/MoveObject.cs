using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float deadZone = 0f;
    private GameManagerScript gameManager;

    private float speed;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        speed = gameManager.carSpeed;

    }

    // Update is called once per frame
    void Update()
    {

        if(speed >=  0)
        {
          transform.position = transform.position + (Vector3.back * speed) * Time.deltaTime;

        }
        //destroyObject();
    }
}
