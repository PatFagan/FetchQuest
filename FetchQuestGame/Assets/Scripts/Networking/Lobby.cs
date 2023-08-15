using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class Lobby : NetworkBehaviour
{
    // variables
    public NetworkManager networkManager;
    public TMP_InputField lobbyNameInput;
    public TMP_InputField usernameInput;
    public GameObject networkingInterface;
    public GameObject optionsInterface;
    public GameObject clientButton, hostButton;
    bool optionsMenuEnabled = false;
    public GameObject lobbyCam;

    void Update()
    {
        // if escape is pressed, open exit lobby menu
        if (Input.GetButtonDown("Escape") && networkingInterface.activeSelf == false)
        {
            optionsMenuEnabled = !optionsMenuEnabled;
            optionsInterface.SetActive(optionsMenuEnabled);
        }
        if (usernameInput.text.Length > 0 && usernameInput.text.Length < 12)
        {
            clientButton.SetActive(true);
            hostButton.SetActive(true);
        }
        else
        {
            clientButton.SetActive(false);
            hostButton.SetActive(false);
        }
    }

    // username
    public static string DisplayName { get; private set; }

    private void Start()
    {
        SetUpUsernameInputField();
    }

    private void SetUpUsernameInputField()
    {
        if (!PlayerPrefs.HasKey("PlayerName")) { return; }

        string defaultName = PlayerPrefs.GetString("PlayerName");

        usernameInput.text = defaultName;
    }

    private void SaveUsername()
    {
        PlayerPrefs.SetString("PlayerName", usernameInput.text);
    }

    public void StartLobbyAsHost()
    {
        networkManager.StartHost();
        networkManager.networkAddress = lobbyNameInput.text;
        print("host:" + networkManager.networkAddress);
        networkingInterface.SetActive(false);
        lobbyCam.SetActive(false);
    }

    public void StartLobbyAsClient()
    {
        networkManager.StartClient();
        networkManager.networkAddress = lobbyNameInput.text;
        print("client:" + networkManager.networkAddress);
        networkingInterface.SetActive(false);
        lobbyCam.SetActive(false);
    }

    public void ExitLobby()
    {
        // check if user is host or client, then exit accordingly
        if (networkManager.mode == NetworkManagerMode.Host)
            networkManager.StopHost();
        else
            networkManager.StopClient();
        networkingInterface.SetActive(true);
        optionsInterface.SetActive(false);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}