using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton //
    public AudioManager S;

    // VCAs //
    FMOD.Studio.VCA master;
    FMOD.Studio.VCA sfx;
    FMOD.Studio.VCA dia;

    // Snapshots // 
    static FMOD.Studio.EventInstance insideLockerSnapshot;

    void Start()
    {
        if (S != null)
        {
            S = this;
        }

        // Get snapshot
        insideLockerSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Inside Locker");
        insideLockerSnapshot.setParameterByName("Intensity", 0);
        float ahh;
        insideLockerSnapshot.getParameterByName("Intensity", out ahh);
        Debug.Log(ahh);
        insideLockerSnapshot.start();

        // MASTER BANK IS LOADED ON GAME INIT. ALL OTHERS SHOULD BE DONE MANUALLY
    }

    static public void toggleInsideLockerSnapshot(bool on)
    {
        if (on)
        {
            insideLockerSnapshot.setParameterByName("Intensity", 100);
        }

        else
        {
            insideLockerSnapshot.setParameterByName("Intensity", 0);
        }
    }
}
