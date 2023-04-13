using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class TrafficSpawner : MonoBehaviour
{
    public List<GameObject> Traffic = new List<GameObject>();

    private PlayerController playerController;
    private bool turnedOn = true;
    private bool willSpawn = true;
    private float requiredPlayerDistance = 20f;
    private float timeBetweenSpawns = 5f;
    private float timeOfLastSpawn = 0f;
    private List<Traffic> registeredTraffic = new List<Traffic>();
    private static List<TrafficSpawner> trafficSpawners = new List<TrafficSpawner>();

    private void Awake()
    {
        trafficSpawners.Add(this);
        playerController = FindAnyObjectByType<PlayerController>();
        timeOfLastSpawn -= timeBetweenSpawns;
    }

    private void Update()
    {
        var distanceToPlayer = (playerController.transform.position - transform.position).magnitude;
        willSpawn = (distanceToPlayer > requiredPlayerDistance) && ((timeOfLastSpawn + timeBetweenSpawns) <= Time.time);
        if (turnedOn && willSpawn)
        {
            SpawnTraffic();
        }
    }

    private void OnDestroy()
    {
        trafficSpawners.Remove(this);
    }

    private void SpawnTraffic()
    {
        var index = Random.Range(0, Traffic.Count - 1);
        var obj = Instantiate(Traffic[index], transform.position, Quaternion.LookRotation(transform.forward), null);
        var traffic = obj.GetComponent<Traffic>();
        traffic.RegisterSpawner(this);
        RegisterTraffic(traffic);
        timeOfLastSpawn = Time.time;
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
        turnedOn = false;
        registeredTraffic.ForEach(t => Destroy(t.gameObject)) ;
        turnedOn = true;
    }

    public static void ResetAllSpawners()
    {
        trafficSpawners.ForEach(ts => ts.Reset());
    }
}
