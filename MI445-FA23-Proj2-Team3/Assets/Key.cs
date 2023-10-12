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
    [SerializeField]
    private GameObject key;

    private GameManager gameManager;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Interact()
    {
        gameManager.flags.Add("greenKey", true);
        key.SetActive(false);
    }
}
