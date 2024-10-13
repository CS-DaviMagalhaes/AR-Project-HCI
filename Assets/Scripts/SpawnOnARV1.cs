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

    private GameObject selectedObject; // El objeto seleccionado para renderizar
    private GameObject spawnedObject;
    private MeshAnalyser meshAnalyser;
    private Mesh arMesh;

    private void Start()
    {
        if (spawnLikelihood == 0) return;

        // Obtener el nombre del modelo seleccionado desde PlayerPrefs
        string modelName = PlayerPrefs.GetString("selectedModel", ""); // Default a un modelo específico si es necesario

        // Encontrar el modelo en la lista
        selectedObject = spawnObjects.Find(obj => obj.name == modelName);

        if (selectedObject == null)
        {
            Debug.LogError("Modelo no encontrado: " + modelName);
            return;
        }

        meshAnalyser = GetComponent<MeshAnalyser>();
        meshAnalyser.analysisDone += StartSpawning;
    }

    private void OnDestroy()
    {
        meshAnalyser.analysisDone -= StartSpawning;
    }

    private void StartSpawning()
    {
        arMesh = GetComponent<MeshFilter>().sharedMesh;

        int spawnLikely = Random.Range(0, 100 / spawnLikelihood);
        if (spawnLikely != 0) return;

        if (arMesh.vertexCount > minVertsForSpawn && meshAnalyser.IsGround)
        {
            InstantiateObject(selectedObject);
        }
    }

    private void InstantiateObject(GameObject obj)
    {
        if (spawnedObject != null) return; // Evita duplicados, instanciando solo una vez

        spawnedObject = Instantiate(obj, GetRandomVector(), Quaternion.identity);
        spawnedObject.transform.localScale *= scaler;
    }

    private Vector3 GetRandomVector()
    {
        Vector3 highestVert = Vector3.zero;
        float highestY = Mathf.NegativeInfinity;

        foreach (var vert in arMesh.vertices)
        {
            if (vert.y > highestY)
            {
                highestY = vert.y;
                highestVert = transform.TransformPoint(vert);
            }
        }

        return highestVert;
    }
}
