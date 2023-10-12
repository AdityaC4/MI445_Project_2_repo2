using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject door;

    [SerializeField]
    private string keyTag;
    
    private GameManager gameManager;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    public void Interact()
    {
        if (gameManager.flags.ContainsKey(keyTag))
        {
            bool flagValue = gameManager.flags[keyTag];
            if (flagValue == true)
            {
                Debug.Log("key found");
                door.SetActive(false);
                return;
            }
        }  
        Debug.Log("no matching key for this door;");
    }
}
