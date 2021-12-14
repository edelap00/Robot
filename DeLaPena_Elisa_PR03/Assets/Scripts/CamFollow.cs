using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // Start is called before the first frame update


    //Variable de tipo transform que está vinculada al robot
    [SerializeField] Transform playerPosition;
    //Variables mov de la cámara
     float distancia = -10f;
    Vector3 cameraPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //  transform.LookAt(playerPosition);
        cameraPosition = new Vector3((playerPosition.position.x), (playerPosition.position.y), distancia);
        transform.position = cameraPosition;
    }
}
