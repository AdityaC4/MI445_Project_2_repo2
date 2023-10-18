using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationCamera : MonoBehaviour, IInteractable
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private CameraController cameraController;

    //private Vector3 originalCameraPos;
    //private Quaternion originalCameraRot;

    [SerializeField]
    private Transform camPos;

    [SerializeField]
    private Transform mainCameraPos;

    private bool interacted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && interacted)
        {
            interacted = false;
            Unlock();
        }
    }



    public void Interact()
    {
        interacted = true;

        playerController.canMove = false;
        cameraController.lockCamera = true;

        //originalCameraPos = Camera.main.transform.position;
        //originalCameraRot = Camera.main.transform.rotation;

        Camera.main.transform.position = camPos.position;
        Camera.main.transform.rotation = camPos.rotation;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Unlock()
    {

        playerController.canMove = true;
        cameraController.lockCamera = false;

        Camera.main.transform.position = mainCameraPos.position;
        Camera.main.transform.rotation = mainCameraPos.rotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
