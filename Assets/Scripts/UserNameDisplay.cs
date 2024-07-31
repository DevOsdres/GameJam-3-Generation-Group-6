using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserNameDisplay : MonoBehaviour
{
    public TextMeshProUGUI userNameText;

    private void Awake()
    {
        // No destruir este objeto al cargar una nueva escena
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Obtener el nombre de usuario de PlayerPrefs
        string currentUserName = PlayerPrefs.GetString("CurrentUserName", "Guest");
        // Actualizar el texto del nombre de usuario
        userNameText.text = "User: " + currentUserName;
    }

    // Método para actualizar el nombre de usuario cuando se cambia
    public void UpdateUserName(string newUserName)
    {
        userNameText.text = "User: " + newUserName;
    }
}

