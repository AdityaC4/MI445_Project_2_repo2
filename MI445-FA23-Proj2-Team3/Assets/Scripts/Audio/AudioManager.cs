using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eLanguage { ENG, SPN };

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
    public DialogueManager dialogueManager;

    [Header("Audio Options")]
    [SerializeField] private eLanguage currentLanguageVO = eLanguage.ENG;
    [SerializeField] private eLanguage currentLanguageSB = eLanguage.ENG;

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
        // Look for player's selected language & load that bank TODO Uncomment out these bois later
        //currentLanguageVO = (eLanguage) PlayerPrefs.GetInt("LanguageVO", (int)eLanguage.ENG);
        //currentLanguageSB = (eLanguage) PlayerPrefs.GetInt("LanguageSB", (int)eLanguage.ENG);
        LoadLanguageBank(currentLanguageVO);
    }

    public void ToggleInsideLockerSnapshot(bool on)
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
        FMODUnity.RuntimeManager.UnloadBank("LocalizedDIA_" + currentLanguageVO.ToString());

        // Load new one
        FMODUnity.RuntimeManager.LoadBank("LocalizedDIA_" + language.ToString());
        PlayerPrefs.SetInt("LanguageVO", (int) currentLanguageVO);
    }

    public void SetGameLanguageVO(eLanguage language)
    {
        currentLanguageVO = language;
        PlayerPrefs.SetInt("LanguageVO", (int) language);
    }

    public eLanguage GetGameLanguageVO()
    {
        return currentLanguageVO;
    }

    public void SetGameLanguageSB(eLanguage language)
    {
        currentLanguageSB = language;
        PlayerPrefs.SetInt("LanguageSB", (int) language);
    }

    public eLanguage GetGameLanguageSB()
    {
        return currentLanguageSB;
    }
}
