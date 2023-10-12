using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour
{
    public GameObject lidClosed;
    public GameObject lidOpen;
    public GameObject note;

    public void Open()
    {
        lidClosed.SetActive(false);
        lidOpen.SetActive(true);
        note.SetActive(true);

        // Play open case sound (attaching to lid because the pivot of this GO is weird)
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Suitcase Open", lidOpen);
    }
}
