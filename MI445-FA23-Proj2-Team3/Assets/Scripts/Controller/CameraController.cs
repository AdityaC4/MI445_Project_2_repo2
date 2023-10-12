using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Setting")]
    public Camera controledCamera;
    public float rotationSpeed = 60f;
    public bool invert = true;

    private InputManager inputManager;
    
    // Start is called before the first frame update
    void Start()
    {
        SetUpCamera();
        SetUpInputManager();
    }

    int waitForFrames = 3;
    int framesWaited = 0;

    // Update is called once per frame
    void Update()
    {
        if (framesWaited <= waitForFrames)
        {
            framesWaited += 1;
            return;
        }
        ProcessRotation();
    }

    void SetUpCamera()
    {
        if (controledCamera == null)
        {
            controledCamera = GetComponent<Camera>();
        }
    }

    void SetUpInputManager()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    void ProcessRotation()
    {
        float verticalLookInput = inputManager.verticalLookAxis;

        Vector3 cameraRotation = controledCamera.transform.rotation.eulerAngles;
        float newXRotation = 0;
        if (invert)
        {
            newXRotation = cameraRotation.x - verticalLookInput * rotationSpeed * Time.deltaTime;
        }
        else
        {
            newXRotation = cameraRotation.x + verticalLookInput * rotationSpeed * Time.deltaTime;
        }

        if (newXRotation < 270 && newXRotation >= 180)
        {
            newXRotation = 270;
        }
        else if (newXRotation > 90 && newXRotation < 180)
        {
            newXRotation = 90;
        }
        controledCamera.transform.rotation = Quaternion.Euler(new Vector3(newXRotation, cameraRotation.y, cameraRotation.z));
    }
}
