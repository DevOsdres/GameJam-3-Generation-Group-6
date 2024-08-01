using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public float levelTime = 300f; // Tiempo en segundos para el nivel
    public TextMeshProUGUI timerText; // Referencia al UI Text para mostrar el tiempo
    public AudioSource backgroundMusic; // Referencia al AudioSource de la música de fondo
    private float remainingTime;

    void Start()
    {
        remainingTime = levelTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerUI();

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                GameOver();
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop(); // Detener la música de fondo
        }
        SceneManager.LoadScene(9); // Carga la escena de Game Over con el índice 9
    }

    public void ReduceTime(float amount)
    {
        remainingTime -= amount;
        if (remainingTime < 0)
        {
            remainingTime = 0;
            GameOver(); // Asegura que si el tiempo llega a cero, se llame a GameOver
        }
        UpdateTimerUI();
    }
}