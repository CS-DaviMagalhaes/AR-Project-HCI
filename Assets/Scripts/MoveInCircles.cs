using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInCircles : MonoBehaviour
{
    public Transform playerCamera;  // Reference to the camera (or player)
    private float speed = 8f;         // Speed of orbiting
    public Vector3 direction = Vector3.up;

    //simple function (speed does)
    void Update()
    {
        // Orbit around the camera
        transform.RotateAround(playerCamera.position, direction, 5* speed * Time.deltaTime);

        // Make the plane look at the camera
        transform.LookAt(playerCamera.position);
    }

}