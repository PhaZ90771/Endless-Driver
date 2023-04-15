using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 thirdPersonOffset = new Vector3(0, 7, -10);
    private Vector3 firstPersonOffset = new Vector3(0, 2, 1);
    private VIEWMODE viewMode = VIEWMODE.THIRDPERSON;
    private PlayerController controller;

    private void Update()
    {
        if (!controller)
        {
            controller = player?.GetComponent<PlayerController>();
            if (controller)
            {
                controller.RegisterCameraFollow(this);
            }
        }
        else
        {
            var playerCode = controller.player == PlayerController.PLAYER.PlayerOne ? "P1" : "P2";
            var swapView = Input.GetButtonDown("Swap View " + playerCode);
            if (swapView)
            {
                viewMode = (viewMode == VIEWMODE.THIRDPERSON) ? VIEWMODE.FIRSTTPERSON : VIEWMODE.THIRDPERSON;
            }
        }
    }

    private void LateUpdate()
    {
        if (controller)
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
            if (viewMode == VIEWMODE.THIRDPERSON)
            {
                transform.position += offset;
            }
            else
            {
                transform.Translate(offset);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (controller)
        {
            var entrance = other.gameObject.GetComponentInParent<EntrancePortal>();
            if (entrance)
            {
                controller.Loop();
            }
        }
    }
    
    private enum VIEWMODE
    {
        THIRDPERSON,
        FIRSTTPERSON
    };
}
