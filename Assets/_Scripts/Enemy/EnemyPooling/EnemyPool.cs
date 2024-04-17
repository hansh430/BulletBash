using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int amountToPool;
    public static EnemyPool Instance;
    public List<GameObject> EnemyPooledObjects = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        GameObject spawnedObject;
        for (int i = 0; i < amountToPool; i++)
        {
            spawnedObject = Instantiate(enemyPrefab);
            spawnedObject.SetActive(false);
            EnemyPooledObjects.Add(spawnedObject);
        }
    }
    public GameObject GetEnemyFromPool()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!EnemyPooledObjects[i].activeInHierarchy)
            {
                return EnemyPooledObjects[i];
            }
        }
        return null;
    }
}
