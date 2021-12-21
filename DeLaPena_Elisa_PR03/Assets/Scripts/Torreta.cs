using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject player;
    [SerializeField] GameObject bala;
    [SerializeField] Transform cannon;
    AudioSource audioSource;
    bool disparando;
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
        if(gameObject.tag == "torreta")
        {
            Vector3 aimVector = player.transform.position + new Vector3(0f, 2f, 1f);
            Vector3 dir = aimVector - transform.position;
            dir.Normalize();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

           float distanciaJugador = Vector3.Distance(player.transform.position, transform.position);

            if(distanciaJugador<20f && !disparando){
                ActivarTorreta();
                disparando = true;
            } else if(distanciaJugador> 20f && disparando)
            {
                DesactivarTorreta();
                
            }
        }

    }

    void Disparar()
    {

        /* if( Mathf.Abs(transform.position.x - player.transform.position.x) < 5f)
       {

       }
       ani.SetBool("dispara", true);
   */

        //evento desde animación de torreta.
        Instantiate(bala, cannon);
        audioSource.Play();
    }

    public void ActivarTorreta()
    {
        ani.SetBool("disparando", true);
    }

    public void DesactivarTorreta()
    {
        ani.SetBool("disparando", false);
    }
}
