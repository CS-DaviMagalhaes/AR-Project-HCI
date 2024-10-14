using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour // Plane controller
{   

    public int score = 0; 
    public Text scoreText;  

    public void UpdateScore()
    {
        score += 5;
        scoreText.text = "Points: " + score;  // Actualiza el texto en la UI
    }

}
