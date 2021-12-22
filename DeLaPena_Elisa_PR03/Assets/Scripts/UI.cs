using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      public void CargarEscena(int scene)
    {
        SceneManager.LoadScene(scene);
    }

      public void Salir()
    {
        Application.Quit();
    }
}
