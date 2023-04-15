using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PLAYER player = PLAYER.PlayerOne;
    public GameModeLoader.GAMEMODE gameMode;

    private static bool IsGameOver = false;

    private readonly float speed = 20.0f;
    private readonly float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;
    private EntrancePortal entrancePortal;
    private ExitPortal exitPortal;
    private FollowPlayer followPlayer;

    private void Awake()
    {
        entrancePortal = FindAnyObjectByType<EntrancePortal>();
        exitPortal = FindAnyObjectByType<ExitPortal>();
    }

    private void Update()
    {
        var playerCode = player == PlayerController.PLAYER.PlayerOne ? "P1" : "P2";
        horizontalInput = Input.GetAxis("Horizontal " + playerCode);
        forwardInput = Input.GetAxis("Vertical " + playerCode);

        transform.Translate(forwardInput * speed * Time.deltaTime * Vector3.forward);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }

    public void Loop()
    {
        TrafficSpawner.ResetAllSpawners();
        ObstacleSpawner.ResetAllSpawners();

        var offset = transform.position - entrancePortal.transform.position;
        transform.position = exitPortal.transform.position + offset;
    }

    public void RegisterCameraFollow(FollowPlayer fp)
    {
        followPlayer = fp;
    }

    private void OnDestroy()
    {
        followPlayer.player = null;
        if (!IsGameOver)
        {
            IsGameOver = true;
            if (gameMode == GameModeLoader.GAMEMODE.Singleplayer)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                if (player == PLAYER.PlayerOne)
                {
                    SceneManager.LoadScene(3);
                }
                else
                {
                    SceneManager.LoadScene(4);
                }
            }
        }
    }

    public enum PLAYER
    {
        PlayerOne,
        PlayerTwo
    }
}