using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    //Extra Variables
    [SerializeField] private LayerMask targetMask;

    //Audio Settings
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flipLightON;
    [SerializeField] private AudioClip flipLightOFF;
    public bool EnemyInRange = false;

    //FlashLight
    public GameObject flashLight;
    private bool flashLightActive = true;


    private Camera cam;
    [SerializeField] private float rayDistance = 4f;
    [SerializeField] LayerMask mask;

    //UI 
    private GUIManager playerUI;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<FPSController>().playerCamera;
        playerUI = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        //Create a ray at the center of the camera that shoots straight (from the Z axis).
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction*rayDistance);

        //Returns the information of the gameobject that the raycast hit.
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, rayDistance, mask))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                        interactable.BaseInteract();
                }
            }
        }

        if (Input.GetKeyDown("f"))
        {
            if (flashLightActive)
            {
                flashLightActive = !flashLightActive;
                audioSource.PlayOneShot(flipLightON);
                flashLight.SetActive(flashLightActive);
            }
            else if (!flashLightActive)
            {
                flashLightActive = !flashLightActive;
                audioSource.PlayOneShot(flipLightOFF);
                flashLight.SetActive(flashLightActive);
            }
        }
    }
}
