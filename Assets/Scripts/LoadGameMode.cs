using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModePicker : MonoBehaviour
{
    private GameModeLoader gameModeLoader;

    private void Awake()
    {
        gameModeLoader = FindAnyObjectByType<GameModeLoader>();
    }

    public void Singleplayer()
    {
        gameModeLoader.gameMode = GameModeLoader.GAMEMODE.Singleplayer;
        LoadLevel();
    }
    public void LocalMultiplayer()
    {
        gameModeLoader.gameMode = GameModeLoader.GAMEMODE.Multiplayer;
        LoadLevel();
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
