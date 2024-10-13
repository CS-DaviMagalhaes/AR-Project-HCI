using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.AR;  // Namespace principal de AR en Lightship 3.9

public class ARModelLoader : MonoBehaviour
{
    public GameObject warPlanePrefab;

    private GameObject currentModel;
    private bool modelPlaced = false;

    // Start is called before the first frame update
    private void Start()
    {
        switch (SceneController.selectedModel)
        {
            case "warPlane":
                currentModel = Instantiate(warPlanePrefab);
                break;
            default:
                Debug.LogError("Modelo no encontrado: " + SceneController.selectedModel);
                return;
        }

        // Inicialmente -> modelo oculto
        currentModel.SetActive(false);
    }

    private void Update()
    {
        if (!modelPlaced && UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Aquí colocaríamos el objeto en la posición tocada si se encuentra una superficie
            Vector3 placementPosition = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
            currentModel.transform.position = placementPosition;
            currentModel.SetActive(true);

            // Fija el modelo en esa posición
            modelPlaced = true;
        }
    }
}
