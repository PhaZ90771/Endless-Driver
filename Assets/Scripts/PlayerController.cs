using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(forwardInput * speed * Time.deltaTime * Vector3.forward);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }

    public void Loop()
    {
        TrafficSpawner.ResetAllSpawners();

        var offset = transform.position - entrancePortal.transform.position;
        transform.position = exitPortal.transform.position + offset;
    }
}