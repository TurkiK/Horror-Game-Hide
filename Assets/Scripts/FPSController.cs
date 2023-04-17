using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 14f;

    public float lookSpeed = 2f;
    public float lookXLimit = 60f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    private bool isMoving = false;
    public bool isRunning;

    public AudioSource audiosource;
    public AudioClip footStep;

    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InvokeRepeating("playFootsteps", 0, 0.6f);
        InvokeRepeating("playRunningFootsteps", 0, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if(Input.GetAxis("Vertical") > 0.2f || Input.GetAxis("Horizontal") > 0.2f || Input.GetAxis("Vertical") < 0f || Input.GetAxis("Horizontal") < 0f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }    

        if(Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    void playFootsteps()
    {
        if (isMoving && !isRunning && characterController.isGrounded)
        {
            audiosource.pitch = Random.Range(0.7f, 1f);
            audiosource.PlayOneShot(footStep);
        }
    }

    void playRunningFootsteps()
    {
        if (isMoving && isRunning && characterController.isGrounded)
        {
            audiosource.pitch = Random.Range(0.9f, 1.4f);
            audiosource.PlayOneShot(footStep);
        }
    }
}
