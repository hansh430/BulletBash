using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDropper : MonoBehaviour
{
    [SerializeField]
    private List<ItemSpawnData> itemToDrop = new List<ItemSpawnData>();
    float[] itemWeights;

    [SerializeField]
    [Range(0, 1)]
    private float dropChance = 0.5f;

    private void Start()
    {
        itemWeights = itemToDrop.Select(item => item.rate).ToArray();
    }
    public void DropItem()
    {
        var dropVariable = Random.value;
        if (dropVariable < dropChance)
        {
            int index = GetRandomWeightedIndex(itemWeights);
            Instantiate(itemToDrop[index].itemPrefab, transform.position, Quaternion.identity);
        }
    }

    private int GetRandomWeightedIndex(float[] itemWeights)
    {
        float sum = 0f;
        for (int i = 0; i < itemWeights.Length; i++)
        {
            sum += itemWeights[i];
        }
        float randomValue = Random.Range(0, sum);
        float tempsum = 0;
        for (int i = 0; i < itemToDrop.Count; i++)
        {
            if (randomValue >= tempsum && randomValue < tempsum + itemWeights[i])
            {
                return i;
            }
            tempsum += itemWeights[i];
        }
        return 0;
    }
}
[Serializable]
public struct ItemSpawnData
{
    [Range(0, 1)]
    public float rate;
    public GameObject itemPrefab;
}
