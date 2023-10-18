using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAudio))]
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 2f;
    public float lookSpeed = 60f;
    public float gravity = -9.81f;

    public bool hidden;
    public Vector3 hiddenEntryPos;

    private CharacterController controller;
    private InputManager inputManager;

    [SerializeField]
    public bool canMove;
   
    [SerializeField]
    public bool isMoving;

    [SerializeField]
    private GameObject normalLight;

    [SerializeField]
    private GameObject revealLight;

    private bool hasCode;

    void Start()
    {
        SetUpCharacterController();
        SetUpInputManager();
        canMove = true;
        isMoving = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void SetUpCharacterController()
    {
        controller = GetComponent<CharacterController>();
        if(controller == null)
        {
            Debug.LogError("The Player controller script does not have a character controller on the same game object");
        }
    }

    void SetUpInputManager()
    {
        inputManager = InputManager.instance;
    }

    void Update()
    {
        // Check if player is moving
        if (controller.velocity == Vector3.zero)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        // Play footstep if moving and grounded
        if (!hidden && isMoving && controller.isGrounded)
        {
            PlayerAudio.PlayFootstep();
        }

        if (canMove)
        {
            ProcessMovement();
            ProcessRotation();
            FlashSwitch();
        }
        
    }

    Vector3 moveDirection;

    void ProcessMovement()
    {
        float leftRightInput = inputManager.horizontalMoveAxis;
        float forwardBackwardInput = inputManager.verticalMoveAxis;

        moveDirection = new Vector3(leftRightInput, 0, forwardBackwardInput);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        moveDirection.y += gravity * Time.deltaTime * 150;

        controller.Move(moveDirection * Time.deltaTime);
    }

    void ProcessRotation()
    {
        float horizontalLookInput = inputManager.horizontalLookAxis;
        Vector3 playerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(playerRotation.x, playerRotation.y + horizontalLookInput * lookSpeed * Time.deltaTime, playerRotation.z));
    }

    public void EnterLocker(Vector3 startPos, Vector3 endPos, Quaternion rotation)
    {
        canMove = false;
        transform.position = endPos;
        hidden = true;
        hiddenEntryPos = startPos;
        //transform.rotation = rotation;
        GetComponent<Rigidbody>().detectCollisions = false;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Locker Enter + Exit");
        AudioManager.S.ToggleInsideLockerSnapshot(true);
    }

    public void ExitLocker(Vector3 endPos)
    {
        StartCoroutine(ExitLockerRoutine(endPos));
    }
    
    IEnumerator ExitLockerRoutine(Vector3 endPos)
    {
        transform.position = endPos;
        hidden = false;
        yield return new WaitForFixedUpdate();
        GetComponent<Rigidbody>().detectCollisions = true;
        canMove = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Locker Enter + Exit");
        AudioManager.S.ToggleInsideLockerSnapshot(false);
    }

    void FlashSwitch()
    {
        if (inputManager.flashTogglePressed)
        {
            if (normalLight.activeSelf)
            {
                normalLight.SetActive(false);
                revealLight.SetActive(true);
            }
            else
            {
                normalLight.SetActive(true);
                revealLight.SetActive(false);
            }
        }
    }
}
