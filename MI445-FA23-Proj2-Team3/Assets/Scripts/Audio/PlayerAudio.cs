using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    // FMOD EventInstances
    FMOD.Studio.EventInstance footstep; // will be used later

    // Footstep vars
    static float footNextTime;

    // TODO Footstep enum

    [Header("Inscribed")]
    static float footstepInterval = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        footNextTime = Time.time + 0.3f;
    }

    /// <summary>
    /// Plays a footstep sfx :) TODO: INCLUDE LOGIC FOR DIFF MATERIAL
    /// /// </summary>
    static public void PlayFootstep()
    {
        if ((footNextTime - Time.time) < 0)
        {
            footNextTime = Time.time + footstepInterval;

            FMODUnity.RuntimeManager.PlayOneShot("event:/Footsteps");
        }
    }
}
