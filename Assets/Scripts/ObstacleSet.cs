using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSet : MonoBehaviour
{
    public void Unload()
    {
        var obstacles = GetComponentsInChildren<Obstacle>().ToList();
        obstacles.ForEach(o => o.transform.parent = null);
        Destroy(this.gameObject);
    }
}
