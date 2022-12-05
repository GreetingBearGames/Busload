using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Define the bus's speed
    [SerializeField] float speed = 10;
    // Define the touch input threshold for moving the bus
    [SerializeField] float touchThreshold = 0.1f;

    // Define the bus's starting position and direction
    Vector3 position;
    Quaternion rotation;

    // This function is called when the script is first loaded
    void Start()
    {
        // Initialize the position and rotation
        position = transform.position;
        rotation = transform.rotation;
    }

    // This function is called every frame of the game
    void Update()
    {
        // Move the bus forward at a constant speed
        position += transform.forward * speed * Time.deltaTime;

        // Update the position and rotation of the bus
        transform.position = position;
        transform.rotation = rotation;

        // Check if the screen is being touched
        if (Input.touchCount > 0)
        {
            // Get the first touch
            Touch touch = Input.GetTouch(0);

            // Check if the touch is moving left or right
            if (touch.deltaPosition.x < -touchThreshold)
            {
                // If it is, rotate the bus to the left
                rotation *= Quaternion.Euler(0, -1, 0);
            }
            else if (touch.deltaPosition.x > touchThreshold)
            {
                // If it is, rotate the bus to the right
                rotation *= Quaternion.Euler(0, 1, 0);
            }
        }
    }


}
