using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject door;
    [SerializeField]
    Vector3 openPos;
    [SerializeField]
    Quaternion openRotation;

    public void Interact()
    {
        Debug.Log("Open Door");
        door.SetActive(false);
        //door.transform.position = openPos;
        //door.transform.rotation = openRotation;
    }
}
