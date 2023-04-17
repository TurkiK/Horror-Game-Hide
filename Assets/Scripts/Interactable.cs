using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //The message that is displayed to the player when looking at an interactable.
    public string promptMessage;

    //This function will be called from our player.
    public void BaseInteract()
    {
        Interact();
    }
    protected virtual void Interact()
    {
        //Template function to be overrriden by subclasses.
    }
}
