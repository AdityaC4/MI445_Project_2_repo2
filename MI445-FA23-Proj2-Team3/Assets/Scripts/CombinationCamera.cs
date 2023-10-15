using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationCamera : MonoBehaviour, IInteractable
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private CameraController cameraController;

    private Vector3 originalCameraPos;
    private Quaternion originalCameraRot;

    [SerializeField]
    private Transform camPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Unlock();
        }
    }



    public void Interact()
    {
        playerController.canMove = false;
        cameraController.lockCamera = true;

        originalCameraPos = Camera.main.transform.position;
        originalCameraRot = Camera.main.transform.rotation;

        // Lerp camera to focus on lock
        Camera.main.transform.position = camPos.position;
        Camera.main.transform.rotation = camPos.rotation;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Unlock()
    {
        playerController.canMove = true;
        cameraController.lockCamera = false;

        Camera.main.transform.position = originalCameraPos;
        Camera.main.transform.rotation = originalCameraRot;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
