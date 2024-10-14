using UnityEngine;
using UnityEngine.UI;

public class BallManager : MonoBehaviour
{
    public int ballCount = 20;  // Contador inicial de bolas
    public Text ballCounterText;  // Referencia al texto de la UI

    void Start()
    {
        // Actualiza el texto en pantalla al inicio
        UpdateBallCounter();
    }

    public void UseBall()
    {
        if (ballCount > 0)
        {
            ballCount--;  // Resta una bola cuando se usa
            UpdateBallCounter();  // Actualiza el contador en la pantalla
        }
        else
        {
            Debug.Log("No more bullets left!");
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
}