using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the CreatureAura FMOD event
/// </summary>
public class CreatureAuraAudio : MonoBehaviour
{
    [Header("Inscribed")]
    [SerializeField] [Tooltip("Please put player in me :(")]
    private GameObject playerGO;
    FMOD.Studio.EventInstance creatureAura;
    [SerializeField] float maxDist;

    [Header("Dynamic")]
    [SerializeField] [Tooltip("Distance between creature and player.")]
    float creatureDistance;
    [SerializeField]float tempDist;

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

        // Grab event and parameter that controls it
        creatureAura = FMODUnity.RuntimeManager.CreateInstance("event:/Creature/Creature Aura");
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("parameter:/Creature Distance", out creatureDistance);

        // creature distance inits at max distance 
        maxDist = creatureDistance;

        // Set 3d space position and start event
        Debug.Log(this.gameObject.transform.position);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(creatureAura, this.gameObject.transform);
        creatureAura.start();
    }

    // Update is called once per frame
    void Update()
    {
        // Update distance between player and creature
        creatureDistance = Vector3.Distance(this.gameObject.transform.position, playerGO.transform.position);

        if (creatureDistance > maxDist)
        {
            creatureDistance = maxDist;
        }

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("parameter:/Creature Distance", creatureDistance);
    }
}
