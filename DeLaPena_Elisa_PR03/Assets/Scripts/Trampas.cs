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
        Torreta1 torreta1 = GetComponent<Torreta1>();
        torretas = GameObject.FindGameObjectsWithTag("torreta1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "robot")
        {
            print("DEntro");
            foreach(GameObject torreta in torretas)
            {
                Torreta1 torreta1 = torreta.GetComponent<Torreta1>();
                // Animator ani = torreta.GetComponent<Animator>();
                torreta1.SendMessage("ActivarTorreta");
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
               
                  torreta1.SendMessage("DesactivarTorreta");

            }
            
        }
    }
}
