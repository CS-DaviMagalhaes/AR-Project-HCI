using System.Collections;
using System.Collections.Generic;
using Niantic.Lightship.AR.NavigationMesh;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private LightshipNavMeshManager _navMeshManager;

    [SerializeField]
    private GameObject planePrefab;

    [SerializeField]
    private float spawnInterval = 4f; // Time interval between spawns
    [SerializeField]
    private int maxPlanes = 10; // Maximum number of planes allowed

    private List<GameObject> _planes = new List<GameObject>(); // List to hold multiple planes
    private List<LightshipNavMeshAgent> _agents = new List<LightshipNavMeshAgent>(); // List to hold agents

    private Coroutine _spawnCoroutine; // Reference to the spawn coroutine

    void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnPlanes()); // Start the coroutine to spawn planes
    }
    
    void Update()
    {
        // Update random destinations for all active planes
        for (int i = 0; i < _agents.Count; i++)
        {
            if (_agents[i] != null)
            {
                // Randomly set a new destination on the mesh for each plane
                Vector3 randomDestination = GetRandomNavMeshPoint();
                _agents[i].SetDestination(randomDestination);
            }
        }
    }

    private IEnumerator SpawnPlanes()
    {
        while (_planes.Count < maxPlanes) // Continuously spawn planes until maxPlanes limit
        {
            SpawnPlane();
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval before spawning the next plane
        }
    }

    private void SpawnPlane()
    {
        // Instantiate a plane and set its position on the NavMesh
        GameObject newPlane = Instantiate(planePrefab);
        newPlane.transform.position = GetRandomNavMeshPoint(); // Place it randomly on the NavMesh
        LightshipNavMeshAgent newAgent = newPlane.GetComponent<LightshipNavMeshAgent>();

        // Add the new plane and agent to the respective lists
        _planes.Add(newPlane);
        if (newAgent != null)
        {
            _agents.Add(newAgent);
        }
        else
        {
            Debug.LogError("LightshipNavMeshAgent not found on the plane prefab!");
        }
    }

    private Vector3 GetRandomNavMeshPoint()
    {
        Vector3 randomPoint = Vector3.zero;
        float radius = 5.0f; // Define a suitable radius for random point generation

        // Try to generate a valid random point within the radius
        for (int i = 0; i < 10; i++)  // Limit the number of attempts to find a valid point
        {
            // Generate a random point within a sphere around the center
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += _navMeshManager.transform.position;

            // Check if the point is on the NavMesh
            if (_navMeshManager.LightshipNavMesh.IsOnNavMesh(randomDirection, 0.2f))
            {
                randomPoint = randomDirection;
                break; // If a valid point is found, break the loop
            }
        }

        return randomPoint;
    }
}
