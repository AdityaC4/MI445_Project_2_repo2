using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange;

    private InputManager inputManager;
    
    // Start is called before the first frame update
    void Start()
    {
        SetUpInput();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.interactPressed)
        {
            if(Physics.Raycast(interactorSource.position, interactorSource.forward, out RaycastHit hitInfo, interactRange))
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }

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
