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

    private ReloadGame reloadGame;

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
        var camera = Instantiate(PlayerCamera, null).GetComponent<Camera>();
        var followPlayer = camera.GetComponent<FollowPlayer>();
        followPlayer.player = player;

        Destroy(this.gameObject);
    }

    public void LoadMultiplayer()
    {
        var playerOne = Instantiate(MultiplayerPlayerOne, null);
        var cameraOne = Instantiate(PlayerCamera, null).GetComponent<Camera>();
        var cameraOneRect = cameraOne.rect;
        cameraOneRect.width = 0.5f;
        cameraOne.rect = cameraOneRect;
        var followPlayerOne = cameraOne.GetComponent<FollowPlayer>();
        followPlayerOne.player = playerOne;

        var playerTwo = Instantiate(MultiplayerPlayerTwo, null);
        var cameraTwo = Instantiate(PlayerCamera, null).GetComponent<Camera>();
        var cameraTwoRect = cameraOne.rect;
        cameraTwoRect.x = 0.5f;
        cameraTwoRect.width = 0.5f;
        cameraTwo.rect = cameraTwoRect;
        var followPlayerTwo = cameraTwo.GetComponent<FollowPlayer>();
        followPlayerTwo.player = playerTwo;

        Destroy(this.gameObject);
    }

    public enum GAMEMODE
    {
        Singleplayer,
        Multiplayer
    }
}
