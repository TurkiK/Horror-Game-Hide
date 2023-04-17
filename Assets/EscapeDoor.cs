using UnityEngine;

public class EscapeDoor : Interactable
{
    public GUIManager playerUI;
    public AudioManager audioManager;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (playerUI.docCount < 4)
            promptMessage = "You cannot escape yet.\nYou have only aquired " + playerUI.docCount + "/7 documents.";
        else
            promptMessage = "Escape?\nYou have aquired " + playerUI.docCount + "/7 documents.";
    }

    protected override void Interact()
    {
        if(playerUI.docCount < 4)
        {
            audioManager.audioSource.PlayOneShot(audioManager.lockedDoor);
        }
        else
        {
            gameManager.winGame();
            audioManager.audioSource.Stop();
            audioManager.audioSource.PlayOneShot(audioManager.winMusic);
        }
    }
}
