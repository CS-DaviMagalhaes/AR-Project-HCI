using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnOnARV1 : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnObjects = new List<GameObject>();
    [SerializeField] private float minVertsForSpawn;
    [SerializeField] private float scaler;

    [Range(0, 100)]
    [SerializeField] private int spawnLikelihood = 33;

    private static bool modelSpawned = false;
    private GameObject selectedObject; // El objeto seleccionado para renderizar
    private GameObject spawnedObject;
    private MeshAnalyser meshAnalyser;
    private Mesh arMesh;

    private void Start()
    {
        // Si el modelo ya fue generado, desactiva este script inmediatamente
        if (PlayerPrefs.GetInt("modelSpawned", 0) == 1)
        {
            this.enabled = false;
            Debug.Log("Modelo ya generado, desactivando script.");
            return;
        }



        // Obtener el nombre del modelo seleccionado desde PlayerPrefs
        string modelName = PlayerPrefs.GetString("selectedModel", ""); // Default a un modelo específico si es necesario

        // Encontrar el modelo en la lista
        selectedObject = spawnObjects.Find(obj => obj.name == modelName);

        if (modelName == "Helicopter")
        {
            scaler = scaler / 4;
        }

        if (selectedObject == null)
        {
            Debug.LogError("Modelo no encontrado: " + modelName);
            return;
        }

        meshAnalyser = GetComponent<MeshAnalyser>();
        if (meshAnalyser != null)
            meshAnalyser.analysisDone += StartSpawning;
    }

    private void OnDestroy()
    {
        if (meshAnalyser != null)
            meshAnalyser.analysisDone -= StartSpawning;
    }

    private void StartSpawning()
    {
        if (PlayerPrefs.GetInt("modelSpawned", 0) == 1) return;

        if (meshAnalyser.IsGround && selectedObject != null)
        {
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 0.8f;
            spawnedObject = Instantiate(selectedObject, spawnPosition, Quaternion.identity);
            spawnedObject.transform.localScale *= scaler;

            modelSpawned = true; // Marcar como completado
            PlayerPrefs.SetInt("modelSpawned", 1); // 1 para indicar que ya fue generado
            PlayerPrefs.Save();

            Debug.Log("Model spawned in front of the camera at: " + spawnPosition);
        }
    }
 }