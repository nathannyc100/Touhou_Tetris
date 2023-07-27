using UnityEngine;
using UnityEngine.UI;
using System;

public class LobbyMenu : MonoBehaviour
{
    private MainMenu mainMenu;
    private GameManager gameManager;

    [SerializeField]
    private GameObject lobbyMenu;
    [SerializeField]
    private GameObject lobby;

    [SerializeField]
    private Button createLobbyButton;
    [SerializeField]
    private Button joinRandomButton;

    public event EventHandler<JoinLobbyEventArgs> JoinLobby;

    public class JoinLobbyEventArgs : EventArgs {
        public GameManager.JoinLobbyMode joinMode;
    }

    private void Awake(){
        mainMenu = GetComponent<MainMenu>();
        gameManager = GameManager.instance;

        mainMenu.OpenLobbyMenu += When_OpenLobbyMenu;

        createLobbyButton.onClick.AddListener(() => { CreateLobby(); }); 
        joinRandomButton.onClick.AddListener(() => { JoinRandomLobby(); });

        lobbyMenu.SetActive(false);
        lobby.SetActive(false);

    }

    private void When_OpenLobbyMenu(object sender, EventArgs e){
        lobbyMenu.SetActive(true);
    }

    private void CreateLobby(){
        Debug.Log("create lobby");
        JoinLobby?.Invoke(this, new JoinLobbyEventArgs { joinMode = GameManager.JoinLobbyMode.CreateNew });
    }

    private void JoinRandomLobby(){
        JoinLobby?.Invoke(this, new JoinLobbyEventArgs { joinMode = GameManager.JoinLobbyMode.JoinRandom });
    }
}
