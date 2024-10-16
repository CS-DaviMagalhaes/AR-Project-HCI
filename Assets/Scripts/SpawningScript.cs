using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject planePrefab; // Reference to the plane prefab
    public float spawnInterval = 4f; // Time interval between spawns

    void Start()
    {
        StartCoroutine(SpawnPlanes()); // Start the coroutine to spawn planes
    }

    private IEnumerator SpawnPlanes()
    {
        while (true) //TODO: change this to stop when the player wins or loses xd
        {
            // Get the camera's position
            Transform cameraTransform = Camera.main.transform;

            // Calculate the horizontal direction behind the player (ignoring camera tilt)
            Vector3 backwardDirection = new Vector3(-cameraTransform.forward.x, 0, -cameraTransform.forward.z).normalized;

            // Offset to spawn the plane behind the player, keeping it at the camera's height
            Vector3 spawnPosition = cameraTransform.position + backwardDirection * 5.0f; // 5 units behind the player
            spawnPosition.y = cameraTransform.position.y;  // Keep the plane at the same height as the camera

            // Instantiate the plane prefab at the calculated position and ensure it faces the camera
            GameObject plane = Instantiate(planePrefab, spawnPosition, Quaternion.identity);

            // Get the MoveInCircles component from the instantiated plane
            MoveInCircles moveScript = plane.GetComponent<MoveInCircles>();
            if (moveScript != null)
            {
                moveScript.playerCamera = cameraTransform; // Set the playerCamera reference
            }
            else
            {
                Debug.LogError("MoveInCircles script not found on the plane prefab!");
            }

            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval before spawning the next plane
        }
    }
}
