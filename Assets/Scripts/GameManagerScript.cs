using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    public float carSpeed = 50f;

    [SerializeField]
    public float oppositeLaneCarSpeed = 40f;

    [SerializeField]
    public float oppositeRightLaneCarSpeed = 40f;

    private int killedZombie = 0;

    [SerializeField]
    public GameObject[] gameObjects;

    [SerializeField]
    public GameObject[] roads;

    [SerializeField]
    public GameObject spawner;


    void Start()
    {
        
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
