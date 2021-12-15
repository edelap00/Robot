using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaMove : MonoBehaviour
{
    float speedBullet = 15f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Autodestruir", 20f);
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speedBullet);
    }

    void Autodestruir()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Robot")
        {
            PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();
           
            playerManager.SendMessage("Morir");
        }

        Destroy(gameObject);
    }
}
