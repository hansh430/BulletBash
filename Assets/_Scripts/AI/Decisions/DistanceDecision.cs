using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDecision : AIDecision
{
    [field: SerializeField]
    [field: Range(0.1f, 15f)]
    public float Distance { get; set; } = 5f;
    public override bool MakeDecision()
    {
        if (Vector3.Distance(enemyAIBrain.Target.transform.position, transform.position) < Distance)
        {
            if (aiActionData.TargetSpotted == false)
            {
                aiActionData.TargetSpotted = true;
            }
        }
        else
        {
            aiActionData.TargetSpotted = false;
        }
        return aiActionData.TargetSpotted;
    }
    protected void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Distance);
            Gizmos.color = Color.white;
        }
    }

}
