using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class TrafficSpawner : MonoBehaviour
{
    public List<GameObject> Traffic = new List<GameObject>();
    
    private static List<TrafficSpawner> trafficSpawners = new List<TrafficSpawner>();
    private static TrafficSpawner activeSpawner = null;

    private const float timeBetweenSpawnsLow = 1f;
    private const float timeBetweenSpawnsHigh = 3f;
    private static float timeBetweenSpawns = 0f;
    private static float timeOfLastSpawn = 0f;

    private PlayerController playerController;
    private bool isTurnedOn = true;
    private float requiredPlayerDistance = 20f;
    private List<Traffic> registeredTraffic = new List<Traffic>();

    private void Awake()
    {
        trafficSpawners.Add(this);
        RandomizeSpawn();

        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        var isActiveSpawner = activeSpawner == this;
        var distanceToPlayer = (playerController.transform.position - transform.position).magnitude;
        var isPlayerDistanceIsEnough = distanceToPlayer > requiredPlayerDistance;
        var timeOfNextSpawn = timeOfLastSpawn + timeBetweenSpawns;
        var isSpawnDelayEnough = timeOfNextSpawn <= Time.time;

        if (isTurnedOn && isActiveSpawner && isPlayerDistanceIsEnough && isSpawnDelayEnough)
        {
            SpawnTraffic();
        }
    }

    private void OnDestroy()
    {
        trafficSpawners.Remove(this);
    }

    private static void RandomizeSpawn()
    {
        var index = Random.Range(0, trafficSpawners.Count);
        activeSpawner = trafficSpawners[index];
        timeBetweenSpawns = Random.Range(timeBetweenSpawnsLow, timeBetweenSpawnsHigh);
    }

    private void SpawnTraffic()
    {
        timeOfLastSpawn = Time.time;
        var index = Random.Range(0, Traffic.Count - 1);
        var obj = Instantiate(Traffic[index], transform.position, Quaternion.LookRotation(transform.forward), null);
        var traffic = obj.GetComponent<Traffic>();
        traffic.RegisterSpawner(this);
        RegisterTraffic(traffic);
        RandomizeSpawn();
    }

    private void RegisterTraffic(Traffic traffic)
    {
        registeredTraffic.Add(traffic);
    }

    public void DeregisterTraffic(Traffic traffic)
    {
        registeredTraffic.Remove(traffic);
    }

    public void Reset()
    {
        isTurnedOn = false;
        registeredTraffic.ForEach(t => Destroy(t.gameObject)) ;
        isTurnedOn = true;
    }

    public static void ResetAllSpawners()
    {
        trafficSpawners.ForEach(ts => ts.Reset());
    }
}
