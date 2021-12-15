using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   Rigidbody2D rb;
        Animator ani;

    //variables 

    bool isGrounded;
    int impulsoV = 8;
    float desplH;
    float maxSpeed = 4f;
    float speed;
    bool mirandoDerecha = true;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        desplH = Input.GetAxis("Horizontal");

        Saltar();

        
       
        Girar();
     

    }

    private void FixedUpdate()
    {
        Andar();
    }

    void Girar()
    {
 //Si nos movemos a la derecha y estamos mirando a la izquierda, volteamos

        if (desplH > 0 && !mirandoDerecha)
        {
            Flip();
        }
        // En caso contrario, si nos movemos a la izquierda y miramos a la derecha
        else if (desplH < 0 && mirandoDerecha)
        {
            // giramos
            Flip();
        }

    }
   void Flip()
        {
            //Cambiamos el valor de la booleana, poniendo su valor contrario
            mirandoDerecha = !mirandoDerecha;
            //Creamos un Vector3 que es igual al de nuestra escala actual
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
       


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
           isGrounded = true;
            ani.SetBool("isGrounded",true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = false;
            ani.SetBool("isGrounded", false);
        }
    }


    void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ani.SetTrigger("Jump");
            rb.AddForce(Vector2.up * impulsoV, ForceMode2D.Impulse);
        }

       
    }

    void Crunch()
    {
        if (Input.GetKeyDown("Space"))
        {

        }

       
    }

    void Correr()
    {
        if (Input.GetKeyDown("Space"))
        {

        }

       
    }

    public void Morir()
    {
        ani.SetTrigger("Morir");

    }

    void Andar()
    {
        
        // Aplicamos el movimiento
        rb.velocity = new Vector2(desplH * maxSpeed, rb.velocity.y);
        //Redondeamos la velocidad para pasarlo al parámetro del animator
        speed = Mathf.Abs(rb.velocity.x);
        ani.SetFloat("SpeedX", speed);

    }
}
