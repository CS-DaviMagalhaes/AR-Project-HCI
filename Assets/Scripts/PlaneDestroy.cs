using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneDestroy : MonoBehaviour // Plane controller
{
    [SerializeField] private GameObject explosionPrefab; // Reference to the explosion prefab

    private ScoreManager scoreManager;
    private BallManager ballManager; // Referencia al BallManager

    private void Start()
    {
        // Inicializa la referencia al BallManager
        ballManager = FindObjectOfType<BallManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target")) // Check for collision with the target
        {
            scoreManager = FindObjectOfType<ScoreManager>();
            scoreManager.UpdateScore();

            // Agrega bolas al destruir el avión
            if (ballManager != null) // Verifica que ballManager esté inicializado
            {
                ballManager.AddBalls(4); // Incrementa la cantidad de bolas
            }

            // Instantiate the explosion at the plane's position
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(collision.gameObject); // Destroy the target object upon collision
            Destroy(gameObject); // Destroy the plane
        }
    }
}
