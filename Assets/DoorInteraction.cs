using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : Interactable
{
    private Door door;
    private void Start()
    {
        door = gameObject.GetComponent<Door>();
        promptMessage = "Open";
    }
    protected override void Interact()
    {
        door.ActionDoor();
    }
}
