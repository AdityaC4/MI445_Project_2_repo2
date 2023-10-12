using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FMODUnity.StudioListener))]
public class PlayerAudio : MonoBehaviour
{
    // FMOD EventInstances
    static public FMOD.Studio.EventInstance footstep; // will be used later

    // Footstep vars
    static float footNextTime;

    // Footstep enum
    enum eFootstepType { Tile, Hardwood, Carpet }

    static eFootstepType footstepType;
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
            footstep = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Footsteps");

            // TODO Find footstep type (for 1st playable, will just use hardwood)
            footstepType = eFootstepType.Hardwood;

            // Set Footstep
            footstep.setParameterByName("Footstep Type", (float) footstepType);
            footNextTime = Time.time + footstepInterval;

            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Footsteps");
        }
    }
}
