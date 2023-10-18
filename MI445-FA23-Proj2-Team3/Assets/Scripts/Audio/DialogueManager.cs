using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles all dialogue and subtitle jazz
/// </summary>
[RequireComponent(typeof(FMODDialogueCallback))]
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager S;

    /// <summary>
    /// Container for dialogue data, both for the spoken line and the subtitles
    /// </summary>
    [System.Serializable]
    private class DialogueLine
    {
        public string engSubtitle;
        public string spnSubtitle;
        public string lineKey;
        public string nextLine;
    }
    [Header("Testing")]
    [Tooltip("Will play test line if true.")]
    [SerializeField] private bool playTestOnStart = false;

    [Header("Inscribed")]
    [SerializeField] private TextAsset dialogueSpreadsheet;
    [SerializeField] private TMP_Text text;

    [Header("Dynamic")]
    [SerializeField] private bool isPlayingDialogue = false;

    // FOR CHECKING PROPER LOADING OF FILES ONLY
    [SerializeField] DialogueLine[] testLines; 
    
    // Data we will actually call from
    Dictionary<string, DialogueLine> dialogueLines;

    // FMOD stuff
    FMOD.Studio.EventInstance currentInstance;
    FMOD.Studio.PLAYBACK_STATE instanceState;
    FMODDialogueCallback dialogueCallback;

    [SerializeField]
    string[] testData;

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

    // Start is called before the first frame update
    void Start()
    {
        // Connect with audio manager in scene
        AudioManager.S.dialogueManager = this;

        // create and populate dialogueLine dict
        dialogueLines = new Dictionary<string, DialogueLine>();
        ParseDialogueSpreadsheet();

        // Get callback
        dialogueCallback = GetComponent<FMODDialogueCallback>();

        // Play test boi
        if (playTestOnStart) PlayDialogue("Test");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayingDialogue)
        {
            currentInstance.getPlaybackState(out instanceState);

            // TODO make logic to chain next line if applicable
            // Clear and disable subtitle boi when dialogue has stopped
            if (instanceState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
            {
                isPlayingDialogue = false;
                text.gameObject.transform.parent.gameObject.SetActive(false);
                text.text = "";
            }
        }
    }

    /// <summary>
    /// Parses through the dialogue spreadsheet to create all dialogueLine objects
    /// in game
    /// </summary>
    private void ParseDialogueSpreadsheet()
    {
        // Cool vars
        string normalizedString;
        DialogueLine tempLine;
        int tableSize;

        // Divide content into each line (and remove \r that mac can generate)
        normalizedString = dialogueSpreadsheet.text.Replace("\r", string.Empty);
        testData = normalizedString.Split(new string[] { "\t", "\n" }, System.StringSplitOptions.None);
        tableSize = testData.Length / 3;

        // parse each line and fill in a dialogue line for each boi (outside header)
        testLines = new DialogueLine[tableSize - 1];

        for (int i = 0; i < tableSize - 1; i++)
        {
            tempLine = new DialogueLine();
            tempLine.engSubtitle = testData[(i + 1) * 3];
            tempLine.spnSubtitle = testData[(i + 1) * 3 + 1];
            tempLine.lineKey     = testData[(i + 1) * 3 + 2];

            testLines[i] = tempLine;
            //Debug.Log(tempLine.lineKey);
            dialogueLines.Add(tempLine.lineKey, tempLine);
        }
    }

    /// <summary>
    /// Handles the playing of the dialogue and subtitles
    /// todo Prevent multiple lines from playing on top of each other
    /// </summary>
    /// <param name="key">The name of the dia line</param>
    public void PlayDialogue(string key)
    {
        // Check if given key is valid
        DialogueLine line;
        try
        {
            line = dialogueLines[key];
        }
        catch 
        {
            Debug.LogError(key + " is not a valid key for a dialogueLine. Cannot play/display line");
            return;
        }

        // Show subtitle
        switch(AudioManager.S.GetGameLanguageSB())
        {
            case eLanguage.ENG:
                text.text = line.engSubtitle;
                break;

            case eLanguage.SPN:
                text.text = line.spnSubtitle;
                break;

            default:
                Debug.LogError("Looks like we don't have anything to display in this language-");
                break;
        }

        // Set text and textbox active
        text.gameObject.transform.parent.gameObject.SetActive(true);

        // Start playing dialogue
        isPlayingDialogue = true;
        currentInstance = dialogueCallback.PlayDialogue(key);
    }
}
