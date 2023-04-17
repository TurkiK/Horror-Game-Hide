using UnityEngine;

public class OfficeDocument : Interactable
{
    public GUIManager playerUI;
    public AudioManager audioManager;
    public GameManager gameManager;

    private void Start()
    {
        playerUI = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManager>();
        audioManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    protected override void Interact()
    {
        audioManager.audioSource.PlayOneShot(audioManager.documentCollect);
        Destroy(gameObject);
        gameManager.updateScore();
        playerUI.DocumentGet();
    }
}
