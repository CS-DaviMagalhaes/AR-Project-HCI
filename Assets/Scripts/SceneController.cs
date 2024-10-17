using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static string selectedModel;

    public void LoadCollectibleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Collectibles");
    }
    
    public void LoadARScene(string modelName)
    {
        // Guardar el nombre del modelo en una variable est�tica
        PlayerPrefs.SetInt("modelSpawned", 0); // 1 para indicar que ya fue generado
        PlayerPrefs.Save();
        selectedModel = modelName;
        PlayerPrefs.SetString("selectedModel", modelName);
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("learnMeshing");
    }
    
    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void LoadGameplay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
}
