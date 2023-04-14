using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PLAYER player = PLAYER.PlayerOne;

    private readonly float speed = 20.0f;
    private readonly float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;
    private EntrancePortal entrancePortal;
    private ExitPortal exitPortal;

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

        var offset = transform.position - entrancePortal.transform.position;
        transform.position = exitPortal.transform.position + offset;
    }

    public enum PLAYER
    {
        PlayerOne,
        PlayerTwo
    }
}