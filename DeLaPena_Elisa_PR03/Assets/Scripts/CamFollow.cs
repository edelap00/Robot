using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // Start is called before the first frame update


    //Variable de tipo transform que está vinculada al robot
    [SerializeField] Transform playerPosition;
    //Variables mov de la cámara
     public float distancia = -10f;
     [SerializeField] float altura=2f;


    Vector3 cameraPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //  transform.LookAt(playerPosition);
        cameraPosition = new Vector3((playerPosition.position.x), (playerPosition.position.y)+altura, distancia);
        transform.position = cameraPosition;
    }
}
