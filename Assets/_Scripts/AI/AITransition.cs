using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    [field: SerializeField]
    public List<AIDecision> Descisions { get; set; }

    [field: SerializeField]
    public AIState PositiveResult { get; set; }

    [field: SerializeField]
    public AIState NegativeResult { get; set; }
    private void Awake()
    {
        Descisions.Clear();
        Descisions = new List<AIDecision>(GetComponents<AIDecision>());
    }
}
