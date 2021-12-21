using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta1 : MonoBehaviour
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
        Animator ani = GetComponent<Animator>();
        audioSource = GetComponent <AudioSource>();
        ani= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if(disparando){
        ani.SetBool("dispara",true); 
        } else {
        ani.SetBool("dispara",false); 
        }

    }

    void Disparar()
    {
        disparando = true;
        //evento desde animaci�n de torreta.
        Instantiate(bala, cannon);
        audioSource.Play();
    }

    public void ActivarTorreta()
    {
       /* if( Mathf.Abs(transform.position.x - player.transform.position.x) < 5f)
        {

        }
        ani.SetBool("dispara", true);
    */
}
    public void DesactivarTorreta()
    {
        ani.SetBool("dispara", false);
    }
}