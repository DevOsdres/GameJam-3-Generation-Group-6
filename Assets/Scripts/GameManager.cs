using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Para cargar escenas o reiniciar

public class GameManager : MonoBehaviour
{
    public int totalTargets = 0;
    private int destroyedTargets = 0;
    public AudioSource backgroundMusic; // Referencia al AudioSource de la música de fondo

    private void OnEnable()
    {
        DestructibleObject.OnObjectDestroyed += OnObjectDestroyed;
    }

    private void OnDisable()
    {
        DestructibleObject.OnObjectDestroyed -= OnObjectDestroyed;
    }

    private void Start()
    {
        // Contar todos los objetivos en la escena
        totalTargets = FindObjectsOfType<DestructibleObject>().Length;
        Debug.Log("Total de objetivos: " + totalTargets);
    }

    private void OnObjectDestroyed()
    {
        destroyedTargets++;
        Debug.Log("Objetivos destruidos: " + destroyedTargets);

        if (destroyedTargets >= totalTargets)
        {
            // Activar condición de victoria
            Victory();
        }
    }

    private void Victory()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop(); // Detener la música de fondo
        }
        SceneManager.LoadScene(7); // Carga la escena de Game Win indice 7
    }
}
