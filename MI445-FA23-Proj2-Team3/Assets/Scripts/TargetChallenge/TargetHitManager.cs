using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetHitManager : MonoBehaviour
{
    public Transform[] targetsOrder;

    private int nextTargetIndex = 0;

    public UnityEvent onAllTargetsHit;

    public static TargetHitManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void TargetHit(Transform target)
    {
        if(target == targetsOrder[nextTargetIndex])
        {
            nextTargetIndex++;
            if(nextTargetIndex >= targetsOrder.Length)
            {
                testc();
                if(onAllTargetsHit != null)
                {
                    onAllTargetsHit.Invoke();
                }
                nextTargetIndex = 0;
            }
        }
        else
        {
            nextTargetIndex = 0;
        }
    }

    public void testc()
    {
        Debug.Log("works");
    }
}
