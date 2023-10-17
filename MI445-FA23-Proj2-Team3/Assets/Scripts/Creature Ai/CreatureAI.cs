using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class CreatureAI : MonoBehaviour
{
    NavMeshAgent agent;
    CreaturePatrol patrol;
    [SerializeField] bool seesPlayer;
    [SerializeField] int aggro;
    [SerializeField] GameObject player;
    [SerializeField] GameObject debugPoint;

    [SerializeField] float visionAngle;
    [SerializeField] float visionDistance;

    [SerializeField] LayerMask visionLayer;

    float accuracy = .5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
        patrol = GetComponent<CreaturePatrol>();
        seesPlayer = false;
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < visionDistance)
        {
            Vector3 targetDir = player.transform.position - transform.position;
            Vector3 forward = transform.forward;
            float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
            if (-visionAngle <= angle && angle <= visionAngle)
            {
                Ray ray = new Ray(transform.position, targetDir);
                ; if (Physics.Raycast(ray, visionDistance, visionLayer))
                {
                    seesPlayer = true;
                }
                else
                {
                    seesPlayer = false;
                }

            }
            else
            {
                seesPlayer = false;
            }
        }
        
    }


    IEnumerator Move()
    {
        while (true)
        {
            if (seesPlayer && !player.GetComponent<PlayerController>().hidden)
            {
                if(aggro < 5)
                {
                    aggro++;
                }
                agent.destination = player.transform.position;
                yield return new WaitForSecondsRealtime(.2f);
            }
            else if (player.GetComponent<PlayerController>().hidden && aggro > 3)
            {

                agent.destination = player.GetComponent<PlayerController>().hiddenEntryPos;
                yield return new WaitForSeconds(1f);
                aggro--;
       
            }
            else if(agent.remainingDistance < accuracy)
            {
                agent.destination = patrol.GetNextPoint();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }

        }
        

    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(agent.destination, Vector3.one);
        }
    }
}
