using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    // Start is called before the first frame update

    bool disparando = false;
    [SerializeField] GameObject player;
    [SerializeField] GameObject bala;
    [SerializeField] Transform cannon;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 aimVector = player.transform.position + new Vector3(0f, 2f, 1f);
        Vector3 dir = aimVector - transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

    }

    void Disparar()
    {
        //evento desde animación de torreta.
        Instantiate(bala, cannon);
    }
}
