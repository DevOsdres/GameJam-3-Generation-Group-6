using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Método que se llama al presionar el botón Play
    public void Play()
    {
        // Genera un número aleatorio entre 1 y 6 (ambos inclusive)
        int randomScene = Random.Range(1, 7);
        // Carga la escena aleatoria
        SceneManager.LoadScene(randomScene);
    }

    // En caso de requerir un botón Exit
    /*public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }*/
}
