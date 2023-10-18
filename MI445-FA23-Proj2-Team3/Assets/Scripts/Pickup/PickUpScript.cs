using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject player;
    public Transform grabPoint;

    private InputManager inputManager;

    public float throwForce = 500f;
    public float pickUpRange = 3f;
    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private bool canDrop = true;
    private int LayerNumber;

    Vector3 initialObjectPos;
    Quaternion initialObjectRot;

    void Start()
    {
        SetUpInput();
        LayerNumber = LayerMask.NameToLayer("holdLayer");
    }
    void Update()
    {
        if (inputManager.interactPressed)
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    if (hit.transform.gameObject.tag == "canPickUp" || hit.transform.gameObject.tag == "ball")
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    StopClipping();
                    DropObject();
                }
            }
        }
        if (heldObj != null)
        {
            MoveObject();

            if (inputManager.leftClickPressed && canDrop == true)
            {
                StopClipping();
                ThrowObject();
            }

        }
    }
    void PickUpObject(GameObject pickUpObj)
    {
        initialObjectPos = pickUpObj.transform.position;
        initialObjectRot = pickUpObj.transform.rotation;
        
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            //heldObjRb.transform.parent = grabPoint.transform;
            heldObj.transform.position = grabPoint.transform.position;
            heldObj.layer = LayerNumber;  
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void DropObject()
    {     
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObj = null;
    }
    void MoveObject()
    {
        heldObj.transform.position = grabPoint.transform.position;
        heldObj.transform.rotation = grabPoint.transform.rotation;
    }
    
    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }
    void StopClipping()
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        if (hits.Length > 1)
        {          
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);  
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
