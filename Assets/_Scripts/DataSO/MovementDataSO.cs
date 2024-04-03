using UnityEngine;

[CreateAssetMenu(menuName ="Agent/MovementData")]
public class MovementDataSO :ScriptableObject
{
    [Range(1f, 10f)]
    public float maxSpeed=5f;
    [Range(0.1f, 100f)]
    public float accleration=50f, deacceleration=50f;
}

