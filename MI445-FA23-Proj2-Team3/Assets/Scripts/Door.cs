using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject door;
    
    private GameManager gameManager;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    public void Interact()
    {
        if (gameManager.flags.ContainsKey("greenKey"))
        {
            bool flagValue = gameManager.flags["greenKey"];
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
