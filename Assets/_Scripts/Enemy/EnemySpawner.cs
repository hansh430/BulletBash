using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private List<GameObject> spawnPoints = null;
    [SerializeField] private int count = 20;
    [SerializeField] private float minDelay = 0.8f, maxDelay = 1.5f;
    private Player player;
    public static int EnemyCount = 0;

    private void Start()
    {
        player = FindObjectOfType<Player>();

        if (spawnPoints.Count > 0)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                SpawnEnemy(spawnPoint.transform.position);
            }
        }
        StartCoroutine(SpawnCoroutine());
    }
    IEnumerator SpawnCoroutine()
    {
        while (!player.IsPlayerDead)
        {
            var randomIndex = Random.Range(0, spawnPoints.Count);
            var randomOffset = Random.insideUnitCircle;
            var spawnpoint = spawnPoints[randomIndex].transform.position + (Vector3)randomOffset;
            SpawnEnemy(spawnpoint);
            var ramdomTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(ramdomTime);
        }
    }

    private void SpawnEnemy(Vector3 spawnPoint)
    {
        GameObject spawnedEnemy = EnemyPool.Instance.GetEnemyFromPool();
        if (spawnedEnemy != null)
        {
            SetEnemyData(spawnPoint, spawnedEnemy);
        }

    }

    private static void SetEnemyData(Vector3 spawnPoint, GameObject spawnedEnemy)
    {
        spawnedEnemy.transform.position = spawnPoint;
        spawnedEnemy.transform.rotation = Quaternion.identity;
        spawnedEnemy.GetComponent<Collider2D>().enabled = true;
        spawnedEnemy.SetActive(true);
        spawnedEnemy.GetComponent<Enemy>().IsEnemyDead = false;
        EnemyCount++;
    }

}
