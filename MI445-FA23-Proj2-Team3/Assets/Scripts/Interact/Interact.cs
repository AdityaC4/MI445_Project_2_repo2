using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

    [SerializeField]
    private Transform playerCameraTransform;
    [SerializeField]
    private Transform objectGrabPointTransform;
    [SerializeField]
    private LayerMask interactable;
    [SerializeField]
    float pickUpDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnInteract()
    {

        Debug.Log("test");  
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, interactable))
        {
            Interactable interactable = raycastHit.transform.GetComponent<Interactable>();
            Debug.Log(interactable);

            if (interactable)
            {
                interactable.Interact();
            }
        }
              
    }
}
