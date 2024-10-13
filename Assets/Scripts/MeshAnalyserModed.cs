using System;
using System.Collections;
using UnityEngine;

public class MeshAnalyserModed : MonoBehaviour
{
    [SerializeField] private float groundThreshold = 0.8f;
    [SerializeField] private float minVerts = 100;
    private bool isGround;
    private MeshFilter _meshFilter;
    private bool analysisCompleted = false;

    public event Action OnSurfaceDetected;

    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        StartCoroutine(CheckForSurface());
    }

    IEnumerator CheckForSurface()
    {
        while (!analysisCompleted)
        {
            yield return new WaitForSeconds(1f);  // Reducir la frecuencia de análisis

            var mesh = _meshFilter.sharedMesh;
            if (mesh != null && mesh.vertexCount >= minVerts && AnalyseSurface(mesh))
            {
                analysisCompleted = true;  // Bloquea análisis adicionales
                Debug.Log("Unified surface detected.");
                OnSurfaceDetected?.Invoke(); // Emitimos el evento una sola vez
            }
        }
    }

    private bool AnalyseSurface(Mesh mesh)
    {
        float avgNormal = 0;
        foreach (var normal in mesh.normals)
            avgNormal += normal.normalized.y;

        avgNormal /= mesh.vertexCount;
        return avgNormal >= groundThreshold;
    }
}
