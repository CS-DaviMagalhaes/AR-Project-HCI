using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour 
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float force = 850.0f;
    [SerializeField] private Transform pointer;
    [SerializeField] private float lifetime = 2.0f; //eliminar la bala despues de algunos segundos

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

                Vector3 direction;
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    direction = hit.point - pointer.position;
                }
                else
                {
                    direction = pointer.forward;
                }

                GameObject ball = Instantiate(bullet, pointer.position, Quaternion.identity, transform);
                ball.GetComponent<Rigidbody>().AddForce(direction.normalized * force);
                ballManager.UseBall(); //gasta una bala

                //eliminar la bala despues de algunos segundos
                Destroy(ball, lifetime);
            }
        }
    }
    
}
