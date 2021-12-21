using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
        Rigidbody2D rb;
        Animator ani;
       AudioSource audioSource;
    //variables 
    bool alive;
    bool isGrounded;
    bool crouch;
    int impulsoV = 8;
    float desplH;
    float maxSpeed = 4f;
    float speed;
    bool mirandoDerecha = true;
    [SerializeField] AudioClip muerte;
    [SerializeField] AudioClip salto;
    [SerializeField] AudioClip golpe;

    // Start is called before the first frame update
    void Start()
    {
       alive= true;
       rb = GetComponent<Rigidbody2D>();
       ani = GetComponent<Animator>();
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    
        desplH = Input.GetAxis("Horizontal");

        if (alive){
        Saltar();
        Girar();
            Crunch();
            Correr();
        }
       
     

    }

    private void FixedUpdate()
    {
    if(alive){
    Andar();
    }   
    }


     void Andar()
    {
        
        // Aplicamos el movimiento
        rb.velocity = new Vector2(desplH * maxSpeed, rb.velocity.y);
        //Redondeamos la velocidad para pasarlo al parámetro del animator
        speed = Mathf.Abs(rb.velocity.x);
        ani.SetFloat("SpeedX", speed);

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


    void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !crouch)
        {
            ani.SetTrigger("Jump");
            rb.AddForce(Vector2.up * impulsoV, ForceMode2D.Impulse);
        }

       
    }

    void Crunch()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            print("Abajo");
            
                ani.SetBool("Crouch", true);
            crouch = true;
                maxSpeed = 2f;
            
           
        }

       if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            print("Arriba");
            ani.SetBool("Crouch", false);
            maxSpeed = 4f;
            crouch = false;
        }


    }

    void Correr()
    {
         if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Cambio la velocidad
            maxSpeed = 7f;
            ani.SetBool("Running", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ani.SetBool("Running", false);
            maxSpeed = 4f;
        }


    }

    public void Morir()
    {
        ani.SetTrigger("Morir");
        
         if(alive){
    alive=false;
      AudioSource.PlayOneShot(muerte, 1f);
    }

   Invoke("Reiniciar", 3f);

    }

       void Reiniciar()
    {
       // SceneManager.LoadScene(1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
           isGrounded = true;
            ani.SetBool("isGrounded",true);
        }

         if (collision.gameObject.layer == 7)
        {
            //emparentar
            transform.parent= collision.gameObject.transform;

            isGrounded = true;
            ani.SetBool("isGrounded",true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = false;
            ani.SetBool("isGrounded",false); 
        }

         if (collision.gameObject.layer == 7)
        {
            transform.parent = null;
             isGrounded = false;
            ani.SetBool("isGrounded",false); 
        }
    }
}
