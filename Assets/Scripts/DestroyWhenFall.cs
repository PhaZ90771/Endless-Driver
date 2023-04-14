using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenFall : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < -100f)
        {
            Destroy(this.gameObject);
        }
    }
}
