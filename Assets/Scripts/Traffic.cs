using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic : MonoBehaviour
{
    private readonly float speed = 20.0f;
    private TrafficSpawner spawner;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    private void OnDestroy()
    {
        spawner.DeregisterTraffic(this);
    }

    public void RegisterSpawner(TrafficSpawner trafficSpawner)
    {
        spawner = trafficSpawner;
    }
}
