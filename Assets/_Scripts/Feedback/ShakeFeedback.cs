using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeFeedback : Feedback
{
    [SerializeField]
    private GameObject objToShake = null;
    [SerializeField]
    private float duration = 0.2f, strength = 1, randomness = 90;
    [SerializeField]
    private int virbrato = 10;
    [SerializeField]
    private bool snapping = false, fadeout = true;
    public override void CompletePreviousFeedback()
    {
        objToShake.transform.DOComplete();
    }

    public override void CreateFeedback()
    {
        CompletePreviousFeedback();
        objToShake.transform.DOShakePosition(duration, strength, virbrato, randomness, snapping, fadeout);
    }
}
