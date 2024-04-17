using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ResourcesData")]
public class ResourcesDataSO : ScriptableObject
{
    [field: SerializeField]
    public ResourcesTypeEnum ResourceType { get; set; }
    [SerializeField]
    private int minAmmount = 1, maxAmmount = 5;
    public int GetAmount()
    {
        return Random.Range(minAmmount, maxAmmount + 1);
    }
}
public enum ResourcesTypeEnum
{
    None,
    Health,
    Ammo
}
