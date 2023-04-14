using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerController;

public class GameModeLoader : MonoBehaviour
{
    public GameObject Singleplayer;
    public GameObject MultiplayerPlayerOne;
    public GameObject MultiplayerPlayerTwo;
    public GameObject PlayerCamera;
    public GAMEMODE gameMode = GAMEMODE.Singleplayer;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        var index = SceneManager.GetActiveScene().buildIndex;
        if (index == 1)
        {
            if (gameMode == GAMEMODE.Singleplayer)
            {
                LoadSingleplayer();
            }
            else
            {
                LoadMultiplayer();
            }
        }
    }

    public void LoadSingleplayer()
    {
        var player = Instantiate(Singleplayer, null);
        var camera = Instantiate(PlayerCamera, null).GetComponent<FollowPlayer>();
        camera.player = player;

        Destroy(this.gameObject);
    }

    public void LoadMultiplayer()
    {
        var playerOne = Instantiate(MultiplayerPlayerOne, null);
        var cameraOne = Instantiate(PlayerCamera, null).GetComponent<FollowPlayer>();
        cameraOne.player = playerOne;

        var playerTwo = Instantiate(MultiplayerPlayerTwo, null);
        var cameraTwo = Instantiate(PlayerCamera, null).GetComponent<FollowPlayer>();
        cameraTwo.player = playerTwo;

        Destroy(this.gameObject);
    }

    public enum GAMEMODE
    {
        Singleplayer,
        Multiplayer
    }
}
