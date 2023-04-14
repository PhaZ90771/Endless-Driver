using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private ObstacleSpawner spawner;

    private void OnDestroy()
    {
        spawner.DeregisterObstacle(this);
    }

    public void RegisterSpawner(ObstacleSpawner obstacleSpawner)
    {
        spawner = obstacleSpawner;
    }
}
