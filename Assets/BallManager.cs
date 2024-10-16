using UnityEngine;
using UnityEngine.UI;

public class BallManager : MonoBehaviour
{
    public int ballCount = 20;  // Contador inicial de bolas
    public Text ballCounterText;  // Referencia al texto de la UI
    public Text gameOverText;     // Referencia al texto de Game Over

    void Start()
    {
        // Actualiza el texto en pantalla al inicio
        UpdateBallCounter();
        gameOverText.gameObject.SetActive(false); // Asegúrate de que el texto de Game Over esté oculto al inicio
    }

    public void UseBall()
    {
        Debug.Log("Using a ball. Current count: " + ballCount); // Verificar si se está llamando
        if (ballCount > 0)
        {
            ballCount--;  // Resta una bola cuando se usa
            UpdateBallCounter();  // Actualiza el contador en la pantalla
        }
        else
        {
            Debug.Log("No more balls left, triggering GameOver."); // Mensaje antes de llamar a GameOver
            GameOver(); // Llama al método GameOver si no quedan bolas
        }
    }

    public void AddBalls(int count)
    {
        ballCount += count;  // Agrega bolas (por ejemplo, cuando derribas un helicóptero)
        UpdateBallCounter();  // Actualiza el contador en la pantalla
    }

    private void UpdateBallCounter()
    {
        ballCounterText.text = ballCount + " Bullets";  // Actualiza el texto en la UI
    }

    private void GameOver()
    {
        Debug.Log("Game Over!"); // Mensaje en la consola
        gameOverText.gameObject.SetActive(true); // Muestra el texto de Game Over
        gameOverText.text = "Game Over"; // Actualiza el texto de Game Over
    }
}
