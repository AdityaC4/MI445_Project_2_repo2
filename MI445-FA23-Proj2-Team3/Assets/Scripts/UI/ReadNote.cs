using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNote : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject player;
    
    public GameObject Note;
    public GameObject FlashLight;

    [SerializeField]
    private bool isLibraryNote;

    private bool libraryNoteFound = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isLibraryNote && libraryNoteFound)
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (isLibraryNote)
        {
            libraryNoteFound = true;
        }
        
        FMODUnity.RuntimeManager.PlayOneShot("event:/Paper Pickup");
        Note.SetActive(true);
        FlashLight.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitButton()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Paper Drop");
        Note.SetActive(false);
        FlashLight.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
}
