using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;


    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    private bool finished;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnNextWave(currentWaveIndex));
    }

    IEnumerator SpawnNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }
    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];

        for (int i = 0; i < currentWave.count; i++)
        {
            if(player == null)
            {
                yield break;
            }

            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Instantiate(randomEnemy, randomSpawnPoint.position, randomSpawnPoint.rotation);

            if(i == currentWave.count - 1)
            {
                finished = true;
            }
            else
            {
                finished = false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);

        }
    }

    private void Update()
    {
        if(finished == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finished = false;
            Debug.Log("Wave" + " " + currentWaveIndex + "finished");

            if(currentWaveIndex+1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(SpawnNextWave(currentWaveIndex));
            }
            else
            {
                Debug.Log("game finished");
            }
           
        }
    }

}
