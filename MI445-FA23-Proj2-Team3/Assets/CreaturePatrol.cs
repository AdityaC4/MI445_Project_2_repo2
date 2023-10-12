using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturePatrol : MonoBehaviour
{
    [SerializeField]
    List<GameObject> wanderPoints = new List<GameObject>();

    // Encounter Variables
    [SerializeField]
    float encounterDelayTime;
    [SerializeField]
    int maxRange;

    [SerializeField] float encounterDistance;
    [SerializeField] float encounterTime;

    GameObject player;

    float lastEncounterTime;
    float totalEncounterTime;

    private void Start()
    {
        player = InputManager.instance.player;
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < encounterDistance)
        {
            totalEncounterTime += Time.deltaTime;
        }
        if(totalEncounterTime > encounterTime)
        {
            totalEncounterTime = 0;
            EncounterPlayer();
        }
    }
    public Vector3 GetNextPoint()
    {
        if (Time.time - lastEncounterTime > encounterDelayTime)
        {
            if (player.GetComponent<PlayerController>().hidden)
            {
                return player.GetComponent<PlayerController>().hiddenEntryPos;
            }
            return player.transform.position;
        }
        else
        {
            List<GameObject> possibleTargets = new List<GameObject>();
            Vector3 playerPos = InputManager.instance.player.transform.position;

            foreach (GameObject point in wanderPoints)
            {
                if (Vector3.Distance(point.transform.position, playerPos) < maxRange - ((maxRange / encounterDelayTime) * (Time.time - lastEncounterTime)))
                {
                    possibleTargets.Add(point);
                }
            }

            Debug.Log(possibleTargets.Count - 1);
            if (possibleTargets.Count == 0 )
            {
                Debug.LogWarning("No Possible Targets");
                if (player.GetComponent<PlayerController>().hidden)
                {
                    return player.GetComponent<PlayerController>().hiddenEntryPos;
                }
                return playerPos;
            }
            return possibleTargets[Random.Range(0, possibleTargets.Count - 1)].transform.position;
        }

    }


    public void EncounterPlayer()
    {
        lastEncounterTime = Time.time;
        Debug.Log("Player Encountered");

    }

    private void OnDrawGizmos()
    {

        foreach (GameObject point in wanderPoints)
        {
            if (Vector3.Distance(point.transform.position, player.transform.position) < maxRange - ((maxRange / encounterDelayTime) * (Time.time - lastEncounterTime)))
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(point.transform.position, Vector3.one);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(point.transform.position, Vector3.one);
            }
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(player.transform.position , maxRange - ((maxRange/encounterDelayTime) * (Time.time - lastEncounterTime)));
    }
}
