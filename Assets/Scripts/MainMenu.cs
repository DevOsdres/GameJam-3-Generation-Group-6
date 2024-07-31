using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject userSelectionCanvas; // Canvas para la selección de usuarios
    public GameObject mainMenuCanvas; // Canvas del menú principal
    public TMP_InputField newUserInputField; // Campo de entrada para un nuevo usuario
    public TextMeshProUGUI feedbackText; // Texto para retroalimentación
    public Transform userListContainer; // Contenedor para los botones de usuarios
    public GameObject userButtonPrefab; // Prefab de los botones de usuario

    private List<string> userNames = new List<string>();

    private void Start()
    {
        // Cargar nombres de usuario desde PlayerPrefs
        LoadUserNames();
        Debug.Log("User names loaded: " + string.Join(", ", userNames));
    }

    public void Play()
    {
        if (userNames.Count > 0)
        {
            ShowUserSelection();
        }
        else
        {
            ShowNewUserInput();
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }

    private void ShowUserSelection()
    {
        mainMenuCanvas.SetActive(false);
        userSelectionCanvas.SetActive(true);
        PopulateUserList();
    }

    private void ShowNewUserInput()
    {
        mainMenuCanvas.SetActive(false);
        userSelectionCanvas.SetActive(true);
        feedbackText.text = "No users found. Please register a new user.";
        newUserInputField.gameObject.SetActive(true);
    }

    public void RegisterNewUser()
    {
        string newUserName = newUserInputField.text.Trim();

        if (!string.IsNullOrEmpty(newUserName) && !userNames.Contains(newUserName))
        {
            userNames.Add(newUserName);
            SaveUserNames();
            feedbackText.text = $"User '{newUserName}' registered successfully!";
            PopulateUserList();
            newUserInputField.text = "";
        }
        else
        {
            feedbackText.text = "Invalid or duplicate username.";
        }
    }

    private void PopulateUserList()
    {
        foreach (Transform child in userListContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (string userName in userNames)
        {
            GameObject userButton = Instantiate(userButtonPrefab, userListContainer);
            userButton.GetComponentInChildren<TextMeshProUGUI>().text = userName;
            userButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SelectUser(userName));
        }
    }

    private void SelectUser(string userName)
    {
        PlayerPrefs.SetString("CurrentUserName", userName);
        PlayerPrefs.Save();
        LoadRandomScene();
    }

    private void LoadRandomScene()
    {
        int randomScene = Random.Range(1, 7);
        SceneManager.LoadScene(randomScene);
    }

    private void LoadUserNames()
    {
        int userCount = PlayerPrefs.GetInt("UserCount", 0);

        for (int i = 0; i < userCount; i++)
        {
            string userName = PlayerPrefs.GetString($"User_{i}");
            userNames.Add(userName);
        }
    }

    private void SaveUserNames()
    {
        PlayerPrefs.SetInt("UserCount", userNames.Count);

        for (int i = 0; i < userNames.Count; i++)
        {
            PlayerPrefs.SetString($"User_{i}", userNames[i]);
        }

        PlayerPrefs.Save();
    }

    public void BackToMainMenu()
    {
        userSelectionCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}