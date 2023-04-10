using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 5, -7);
    private PlayerController controller;

    private void Awake()
    {
        if (!player)
        {
            Debug.LogError("Missing reference to player GameObject");
        }
        controller = player.GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        var entrance = other.gameObject.GetComponentInParent<EntrancePortal>();
        if (entrance)
        {
            controller.Loop();
        }
    }
}
