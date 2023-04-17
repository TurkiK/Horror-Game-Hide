using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    //Audio
    public AudioSource audioSource;
    public AudioClip ambMusic;
    public AudioClip chaseMusic;
    public AudioClip documentCollect;
    public AudioClip lockedDoor;
    public AudioClip winMusic;


    //External Refrences
    [SerializeField] private ActionManager player;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(ambMusic);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ActionManager>();
    }

    public void swapAudio()
    {
        if(audioSource.clip.name == "chaseMusic")
        {
            StartCoroutine(AudioFadeOut());
        }
    }

    public IEnumerator AudioFadeOut()
    {
        float startVolume = 0.3f;
        float fadeTime = 5f;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = 0.3f;
        audioSource.clip = ambMusic;
        audioSource.Play();
    }
}