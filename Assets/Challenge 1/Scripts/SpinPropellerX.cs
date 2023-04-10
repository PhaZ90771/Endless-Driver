using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPropellerX : MonoBehaviour
{
    private float rotationSpeed = -3557.0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }
}
