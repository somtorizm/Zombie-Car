using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class ButtonInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isButtonDown = false;
    public bool isButtonUp= false;


    public void OnPointerDown(PointerEventData eventData)
    {
       
        isButtonDown = true;
        isButtonUp = false;

        Debug.Log("Button Down!");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonDown = false;
        isButtonUp = true;


    Debug.Log("Button Up!");
    }

    // Update is called once per frame
    void Update()
    {
        if (isButtonDown)
        {
            Debug.Log("Button is being held down!");

        }
    }
}
