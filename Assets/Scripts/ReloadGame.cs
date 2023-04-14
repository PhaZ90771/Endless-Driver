using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadGame : MonoBehaviour
{
    public GameModeLoader gameModeLoaderPrefab;

    public GameModeLoader.GAMEMODE gameMode;

    private void Update()
    {
        
    }

    public void Reload()
    {
        Instantiate(gameModeLoaderPrefab, null);
        SceneManager.LoadScene(1);
    }
}
