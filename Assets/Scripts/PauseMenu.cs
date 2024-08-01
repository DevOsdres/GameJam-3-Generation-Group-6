using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    public static bool isPaused = false;
    public GameObject pauseMenuCanvas;

    void Start()
    {
        AudioManager.Instance.AssignSliders(musicSlider, sfxSlider);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Play();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuCanvas.SetActive(true);
        isPaused = true;
    }

    public void Play()
    {
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
        isPaused = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    /*public void MainMenu()
    {
        SceneManager.LoadScene(0); // Cargar la escena principal (menú)
    }*/

    public void RestartGame()
    {
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene(0); // Cargar la escena principal (menú)
    }
}
