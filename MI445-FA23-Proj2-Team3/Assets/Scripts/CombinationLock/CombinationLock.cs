using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombinationLock : MonoBehaviour
{
    [Header("Dials")]
    [SerializeField]
    private Dial[] dials;

    [SerializeField]
    private string combination;

    [SerializeField]
    private UnityEvent onCorrectCombinationFound;

    [SerializeField]
    private GameObject hitbox;

    public void CheckCombination(Dial dial)
    {
        for(int i = 0; i<dials.Length; i++)
        {
            int combinationNumber = int.Parse(combination[i].ToString());
            if(combinationNumber != dials[i].GetNumber())
            {
                dial.Unlock();
                return;
            }
        }
        hitbox.SetActive(false);
        CorrectCombination();
    }
    
    private void CorrectCombination()
    {
        for (int i = 0; i < dials.Length; i++)
        {
            dials[i].Lock();
        }
        if(onCorrectCombinationFound != null)
        {
            onCorrectCombinationFound.Invoke();
            Debug.Log("combination found!!!");
        }
    }
}
