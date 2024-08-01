using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDisplay : MonoBehaviour
{
    public Text userNameText; // Texto para mostrar el nombre de usuario

    private void Start()
    {
        string currentUserName = PlayerPrefs.GetString("CurrentUserName", "Guest");
        userNameText.text = $"User: {currentUserName}";
    }
}
