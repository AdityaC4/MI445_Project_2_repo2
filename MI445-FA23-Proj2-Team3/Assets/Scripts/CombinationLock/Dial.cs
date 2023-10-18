using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dial : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float animationDuration;
    private bool isRotating = false;
    private int currentIndex;

    [Header("Events")]
    [SerializeField]
    private UnityEvent<Dial> onDialRotated;

    [SerializeField]
    private PlayerController playerController;

    private InputManager inputManager;

    void Start()
    {
        SetUpInput();
        currentIndex = Random.Range(0, 10);
        transform.localRotation = Quaternion.Euler(0, currentIndex * -36, 0);
    }

    private void Update()
    {
        if (inputManager.leftClickPressed)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        this.Rotate();
                    }
                }
            }
        }
    }

    public void Rotate()
    {
        if (isRotating)
        {
            return;
        }

        isRotating = true;

        // reset
        currentIndex++;
        if(currentIndex >= 10)
        {
            currentIndex = 0;
        }

        LeanTween.cancel(gameObject);
        LeanTween.rotateAroundLocal(gameObject, Vector3.up, -36, animationDuration).setOnComplete(RotationCompleteCallback);
    }

    private void RotationCompleteCallback()
    {
        if(onDialRotated != null)
        {
            onDialRotated.Invoke(this);
        }
    }

    public int GetNumber()
    {
        return currentIndex;
    }

    public void Lock()
    {
        isRotating = true;
    }

    public void Unlock()
    {
        isRotating = false;
    }

    //public void Interact()
    //{ 

    //    this.Rotate();  // change this to mouse click

    //}

    void SetUpInput()
    {
        if (inputManager == null)
        {
            inputManager = FindObjectOfType<InputManager>();
        }
        if (inputManager == null)
        {
            Debug.LogError("There is no input manager in the scene.");
        }
    }
}
