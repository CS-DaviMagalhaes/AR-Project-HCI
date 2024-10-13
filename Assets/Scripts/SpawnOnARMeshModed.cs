using System.Collections.Generic;
using UnityEngine;

public class SpawnOnARMeshModed : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnableModels;
    [SerializeField] private float scaler = 1f;
    private GameObject selectedModel;
    private GameObject spawnedObject;
    private MeshAnalyserModed meshAnalyserModed;
    private bool modelSpawned = false;  // Nueva bandera para bloquear futuras instancias

    void Start()
    {
        string modelName = PlayerPrefs.GetString("selectedModel", "");
        selectedModel = spawnableModels.Find(model => model.name == modelName);

        if (selectedModel == null)
        {
            Debug.LogError("Selected model not found: " + modelName);
            return;
        }

        meshAnalyserModed = GetComponentInChildren<MeshAnalyserModed>();
        if (meshAnalyserModed != null)
            meshAnalyserModed.OnSurfaceDetected += SpawnOnUnifiedSurface;
    }

    private void OnDestroy()
    {
        if (meshAnalyserModed != null)
            meshAnalyserModed.OnSurfaceDetected -= SpawnOnUnifiedSurface;
    }

    private void SpawnOnUnifiedSurface()
    {
        if (modelSpawned || spawnedObject != null) return;  // Evita instancias adicionales

        Vector3 spawnPosition = GetMeshCenterPoint();
        spawnedObject = Instantiate(selectedModel, spawnPosition, Quaternion.identity);
        spawnedObject.transform.localScale *= scaler;
        modelSpawned = true;  // Marca como completado
        Debug.Log("Model spawned at center of surface: " + spawnPosition);
    }

    private Vector3 GetMeshCenterPoint()
    {
        Mesh mesh = meshAnalyserModed.GetComponent<MeshFilter>().sharedMesh;
        Vector3 centerPoint = Vector3.zero;

        foreach (var vert in mesh.vertices)
        {
            centerPoint += meshAnalyserModed.transform.TransformPoint(vert);
        }

        return centerPoint / mesh.vertexCount;
    }
}
