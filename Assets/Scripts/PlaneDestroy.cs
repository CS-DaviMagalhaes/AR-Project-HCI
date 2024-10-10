using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDestroy : MonoBehaviour //ball controller
{   
    [SerializeField] private float autoDestroyTime = 20.0f; // Added semicolon
    private float timer = 0; 

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > autoDestroyTime)
        {
            Destroy(gameObject); // Use lowercase 'gameObject' for the current object
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target")) // Changed to lowercase 'gameObject'
        {
            Destroy(collision.gameObject); // Destroy the object upon collision
        }
    }
}
