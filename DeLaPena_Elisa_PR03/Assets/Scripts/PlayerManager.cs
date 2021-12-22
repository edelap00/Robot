using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
        Rigidbody2D rb;
        Animator ani;
       AudioSource audioSource;
       BoxCollider2D collider;

    //variables 
    bool alive;
    bool libre;
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
    [SerializeField] AudioClip hero;
    [SerializeField] AudioClip timbre;
    Renderer rend;
    [SerializeField] GameObject engranaje;
     [SerializeField] GameObject tapa;
     [SerializeField] GameObject membrana;
     Vector3 membranaPos= new Vector3 (23.7f, 10.2f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        libre = false;
       alive= true;
       rb = GetComponent<Rigidbody2D>();
       ani = GetComponent<Animator>();
       audioSource = GetComponent<AudioSource>();

       //collider
       collider = GetComponent<BoxCollider2D>();
        collider.offset = new Vector2(0.04f, 1.24f);
        collider.size = new Vector2(1.2f, 2.23f);
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
        UpdateCollider();
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
            audioSource.PlayOneShot(salto, 1f);
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

      if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftControl))
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
         audioSource.PlayOneShot(muerte, 1f);
         if(alive){
        alive=false;
        collider.offset = new Vector2(-0.05f, 0.64f);
        collider.size = new Vector2(2.41f, 1.03f);
           }

   Invoke("Reiniciar", 3f);

    }

       void Reiniciar()
    {
       SceneManager.LoadScene(2);
    }

    void UpdateCollider(){
   /* 
   Aquí quería intentar modificar el collider con un switch, pero ya que desde casa no veo ni la consola, me está costando
   Recurro a usar este método para el crouch y en muerte otra actualización
   
   switch(ani.GetCurrentAnimatorStateInfo(0).IsName{
    case "crouch":
     collider.offset = new Vector2(0.41f, 0.9f);
            collider.size = new Vector2(1.46f, 1.54f);
    break;
    */
    

     if (ani.GetCurrentAnimatorStateInfo(0).IsName("r_crouch") || ani.GetCurrentAnimatorStateInfo(0).IsName("r_crouch_stop"))
        {
            collider.offset = new Vector2(0.41f, 0.9f);
            collider.size = new Vector2(1.46f, 1.54f);
        }
        else
        {
            collider.offset = new Vector2(0.04f, 1.24f);
            collider.size = new Vector2(1.2f, 2.23f);
        }
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {

    if (collision.gameObject.layer == 10)
        {audioSource.PlayOneShot(timbre, 1f);
          Abrir();
        }

        if (collision.gameObject.layer == 6)
        {
           isGrounded = true;
            ani.SetBool("isGrounded",true);
        }

         if (collision.gameObject.layer == 8)
        {
        audioSource.PlayOneShot(muerte, 1f);
        //Destroy(gameObject);
       rend.enabled=false;
        
        Invoke("Reiniciar",2f);
         }

         if (collision.gameObject.layer == 7)
        {
            //emparentar
            transform.parent= collision.gameObject.transform;

            isGrounded = true;
            ani.SetBool("isGrounded",true);
        }
        if (collision.gameObject.layer == 9)
        {

        if(!libre){
            audioSource.Stop();
            audioSource.PlayOneShot(hero, 1f);
            Instantiate (membrana, membranaPos , Quaternion.identity);
        libre=true;
        ani.SetTrigger("Rodar");
        }
            Invoke("Final",12f);
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

    void Abrir()
    {
        Destroy(tapa);
        Destroy(engranaje);
    }

   void Final(){
   SceneManager.LoadScene(3);
    }
  }

