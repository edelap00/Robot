using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampas : MonoBehaviour
{

   [SerializeField] GameObject[] torretas;
    [SerializeField] string tagTotal;
 
    // Start is called before the first frame update
    void Start()
    {
          torretas = GameObject.FindGameObjectsWithTag(tagTotal);
        print(torretas.Length);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "robot")
        {
            print("Dentro");
            foreach(GameObject torreta in torretas)
            {
                Torreta1 torreta1 = torreta.GetComponent<Torreta1>();
                 // torreta1.SendMessage("ActivarTorreta"); no me está funcionando con Script

                 Animator ani = torreta.GetComponent<Animator>();
                 
                    ani.SetBool("dispara", true);
              
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {


        if (collision.gameObject.name == "robot")
        { 
            print("Fuera");
            foreach (GameObject torreta in torretas)
            {
                Torreta1 torreta1 = torreta.GetComponent<Torreta1>();
               //   torreta1.SendMessage("DesactivarTorreta");

               Animator ani = torreta.GetComponent<Animator>();
                
                    ani.SetBool("dispara", false);

            }
            
        }
    }
}
