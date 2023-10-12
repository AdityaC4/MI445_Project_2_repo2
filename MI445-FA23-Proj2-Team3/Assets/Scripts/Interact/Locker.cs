using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour,IInteractable
{
    bool isUsed;
    [SerializeField]
    GameObject entryPoint;

    private void Start()
    {
        isUsed = false;
    }
    public void Interact()
    {
        if (isUsed)
        {
            InputManager.instance.player.GetComponent<PlayerController>().ExitLocker(entryPoint.transform.position);
            isUsed = false; 
        }
        else
        {
            InputManager.instance.player.GetComponent<PlayerController>().EnterLocker(entryPoint.transform.position, transform.position, transform.rotation);
            isUsed = true;
        }
        
    }
}
