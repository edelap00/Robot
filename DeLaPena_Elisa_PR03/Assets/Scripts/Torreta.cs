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
    AudioSource audioSource;
    Animator ani;

    void Start()
    {
        audioSource= GetComponent <AudioSource>();
        ani= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 aimVector = player.transform.position + new Vector3(0f, 2f, 1f);
        Vector3 dir = aimVector - transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        if(disparando){
        ani.SetBool("disparando",true); 
        } else {
        ani.SetBool("disparando",false); 
        }

    }

    void Disparar()
    {
        //evento desde animación de torreta.
        Instantiate(bala, cannon);
        audioSource.Play();
    }
}
