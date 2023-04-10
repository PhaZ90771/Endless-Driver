using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float speed = 20.0f;
    private readonly float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(forwardInput * speed * Time.deltaTime * Vector3.forward);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }
}