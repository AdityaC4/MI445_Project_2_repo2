using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eLanguage { ENG, SPN };

[RequireComponent(typeof(FMODDialogueCallback))]
public class AudioManager : MonoBehaviour
{
    // Singleton //
    static public AudioManager S;

    // VCAs //
    FMOD.Studio.VCA master;
    FMOD.Studio.VCA sfx;
    FMOD.Studio.VCA dia;

    // Snapshots // 
    private FMOD.Studio.EventInstance insideLockerSnapshot;

    // Other //
    FMODDialogueCallback dialogueCallback;

    [Header("Audio Options [ do NOT edit plz >:( ]")]
    [SerializeField]
    private eLanguage currentLanguage = eLanguage.ENG;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.LogWarning("Already have an audiomanager. Deleting this boi");
            Destroy(this);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // snapshots
        insideLockerSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Inside Locker");
        insideLockerSnapshot.setParameterByName("Intensity", 0);
        insideLockerSnapshot.start();

        // MASTER BANK IS LOADED ON GAME INIT. ALL OTHERS SHOULD BE DONE MANUALLY //
        // Look for player's selected language & load that bank todo comment back in line below
        //currentLanguage = (eLanguage) PlayerPrefs.GetInt("Language", (int)eLanguage.ENG);
        LoadLanguageBank(currentLanguage);

        // Create dialogue callback boi
        dialogueCallback = GetComponent<FMODDialogueCallback>();
        dialogueCallback.PlayDialogue("Test");
    }

    public void toggleInsideLockerSnapshot(bool on)
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

    public void LoadLanguageBank(eLanguage language)
    {
        // Unload current bank
        FMODUnity.RuntimeManager.UnloadBank("LocalizedDIA_" + currentLanguage.ToString());

        // Load new one
        FMODUnity.RuntimeManager.LoadBank("LocalizedDIA_" + language.ToString());
        PlayerPrefs.SetInt("Language", (int) currentLanguage);
    }

    /// <summary>
    /// Pretty much just a passthrough for the dialogue callback
    /// </summary>
    /// <param name="key">The audio file to play</param>
    public void PlayDialogue(string key)
    {
        dialogueCallback.PlayDialogue(key);
    }
}
