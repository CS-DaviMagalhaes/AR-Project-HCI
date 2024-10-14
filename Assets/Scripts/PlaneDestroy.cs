using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneDestroy : MonoBehaviour // Plane controller
{   
    [SerializeField] private GameObject explosionPrefab; // Reference to the explosion prefab

    private ScoreManager scoreManager; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target")) // Check for collision with the target
        {   
            scoreManager = FindObjectOfType<ScoreManager>();
            scoreManager.UpdateScore();

            // Instantiate the explosion at the plane's position
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            
            Destroy(collision.gameObject); // Destroy the target object upon collision
            Destroy(gameObject); // Destroy the plane
        }
    }
}
