using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameMode : MonoBehaviour
{
    public void LoadSingleplayer()
    {
        // Load Here
        LoadLevel();
    }
    public void LoadLocalMultiplayer()
    {
        // Load Here
        LoadLevel();
    }

    private void LoadLevel()
    {
        // Load Here
        SceneManager.LoadScene(1);
    }
}
