using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAuraTest : MonoBehaviour
{
    [Header("Inscribed")]
    [SerializeField] [Tooltip("Please put player in me :(")]
    private GameObject playerGO;
    FMOD.Studio.EventInstance creatureAura;

    [Header("Play around w/ these")]
    [Range(0,1000)]
    [Tooltip("Max distance from creature for the aura to be heard")]
    [SerializeField] public float maxDistance = 35;

    [Range(0,1000)]
    [Tooltip("Minimum distance from creature for the player to be completely immersed in the aura")]
    [SerializeField] public float minDistance = 5.2f;

    // Start is called before the first frame update
    void Start()
    {
        // Try to find player if it was not provided
        if (playerGO == null)
        {
            playerGO = Camera.main.gameObject.GetComponentInParent<PlayerController>().gameObject;

            // Get PISSED if still can't find it
            if (playerGO == null)
            {
                Debug.LogError("I can't FIND the player, you did not GIVE ME the player, " +
                    "this creature aura junk ain't gonna work. NO COOL AUDIO FOR YOU >:(");
            }
        }

        // Grab event and set dist parameters
        creatureAura = FMODUnity.RuntimeManager.CreateInstance("event:/Test Audio/TestAura");
        creatureAura.setParameterByName("Max Dist", maxDistance);
        creatureAura.setParameterByName("Min Dist", minDistance);

        // Attach event to creature
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(creatureAura, this.gameObject.transform);
        creatureAura.start();
        creatureAura.release();
    }

    // Update is called once per frame
    void Update()
    {
        // Keep these updated
        creatureAura.setParameterByName("Max Dist", maxDistance);
        creatureAura.setParameterByName("Min Dist", minDistance);
    }
}
