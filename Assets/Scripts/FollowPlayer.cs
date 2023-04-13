using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 thirdPersonOffset = new Vector3(0, 5, -7);
    private Vector3 firstPersonOffset = new Vector3(0, 2, 1);
    private VIEWMODE viewMode = VIEWMODE.THIRDPERSON;
    private PlayerController controller;

    private void Awake()
    {
        if (!player)
        {
            Debug.LogError("Missing reference to player GameObject");
        }
        controller = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        var swapView = Input.GetButtonDown("SwapView");
        if (swapView)
        {
            viewMode = (viewMode == VIEWMODE.THIRDPERSON) ? VIEWMODE.FIRSTTPERSON : VIEWMODE.THIRDPERSON;
        }
    }

    private void LateUpdate()
    {
        var offset = 
            viewMode == VIEWMODE.THIRDPERSON ? 
            thirdPersonOffset : 
            firstPersonOffset;
        var rotation = 
            viewMode == VIEWMODE.THIRDPERSON ? 
            Quaternion.Euler(15f, 0f, 0f) : 
            Quaternion.LookRotation(controller.transform.forward);
        transform.rotation = rotation;
        transform.position = player.transform.position;
        transform.Translate(offset);
    }

    private void OnTriggerEnter(Collider other)
    {
        var entrance = other.gameObject.GetComponentInParent<EntrancePortal>();
        if (entrance)
        {
            controller.Loop();
        }
    }
    
    private enum VIEWMODE
    {
        THIRDPERSON,
        FIRSTTPERSON
    };
}
