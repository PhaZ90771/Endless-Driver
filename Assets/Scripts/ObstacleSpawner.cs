using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> ObstaclePrefabs = new List<GameObject>();

    private static List<ObstacleSpawner> obstacleSpawners = new List<ObstacleSpawner>();
    private List<Obstacle> registeredObstacles = new List<Obstacle>();

    private bool isTurnedOn = true;

    private void Awake()
    {
        obstacleSpawners.Add(this);
    }

    private void Update()
    {
        if (isTurnedOn)
        {
            SpawnObstacle();
            isTurnedOn = false;
        }
    }

    private void SpawnObstacle()
    {
        var index = Random.Range(0, ObstaclePrefabs.Count);
        var obj = Instantiate(ObstaclePrefabs[index], transform.position, Quaternion.LookRotation(transform.forward), null);
        var obstacles = obj.GetComponentsInChildren<Obstacle>().ToList();
        obstacles.ForEach(o => o.RegisterSpawner(this));
        obstacles.ForEach(o => RegisterObstacle(o));
        var obstacleSet = obj.GetComponent<ObstacleSet>();
        obstacleSet?.Unload();
    }

    private void RegisterObstacle(Obstacle obstacle)
    {
        registeredObstacles.Add(obstacle);
    }

    public void DeregisterObstacle(Obstacle obstacle)
    {
        registeredObstacles.Remove(obstacle);
    }

    private void OnDestroy()
    {
        obstacleSpawners.Remove(this);
    }

    private void Reset()
    {
        registeredObstacles.ForEach(o => Destroy(o.gameObject));
        isTurnedOn = true;
    }

    public static void ResetAllSpawners()
    {
        obstacleSpawners.ForEach(os => os.Reset());
    }
}
