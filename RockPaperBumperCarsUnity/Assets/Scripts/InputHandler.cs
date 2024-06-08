using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    CarController carController;

    void Awake()
    {
        carController = GetComponent<CarController>();
    }

    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Joystick1Button0)) inputVector.y = -1;
        else if (Input.GetKey(KeyCode.Joystick1Button1)) inputVector.y = 1;

        carController.SetInputVector(inputVector);
    }
}
