using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitDetector : MonoBehaviour
{
    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ball")
        {
            HitTarget(this.gameObject, collision.gameObject);
        }
    }

    void HitTarget(GameObject hitTarget, GameObject ball)
    {
        Debug.Log("Hit target: " + hitTarget.name + " by ball: " + ball.name);
        TargetHitManager.instance.TargetHit(this.transform);
    }
}
