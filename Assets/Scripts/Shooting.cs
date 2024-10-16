using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float force = 850.0f;
    [SerializeField] private float lifetime = 2.0f; //eliminar la bala después de algunos segundos

    private Camera aRcamera;
    private BallManager ballManager; // Referencia al BallManager

    // Start is called before the first frame update
    void Start()
    {
        aRcamera = GameManager.Instance.ARCamera;
        ballManager = FindObjectOfType<BallManager>(); // Encuentra el BallManager en la escena
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (ballManager.ballCount > 0)
            {
                Vector3 inputPosition = Input.GetMouseButtonDown(0) ? Input.mousePosition : Input.GetTouch(0).position;

                Ray ray = aRcamera.ScreenPointToRay(inputPosition);

                // Instanciar la bola en la posición del toque
                GameObject ball = Instantiate(bullet, ray.origin, Quaternion.identity, transform);

                Vector3 direction;
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    direction = hit.point - ray.origin; // Calcula la dirección desde el punto de toque
                }
                else
                {
                    direction = ray.direction; // Si no hay impacto, usa la dirección del rayo
                }

                ball.GetComponent<Rigidbody>().AddForce(direction.normalized * force);
                ballManager.UseBall(); // Gasta una bala

                // Eliminar la bala después de algunos segundos
                Destroy(ball, lifetime);
            }
        }
    }
}
