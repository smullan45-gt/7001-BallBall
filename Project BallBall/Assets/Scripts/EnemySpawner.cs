using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    private GameObject[] spawnerList;

    public GameObject[] powerupList;
    public GameObject enemyPrefab;

    private int chanceOfPowerup = 8;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnerList = GameObject.FindGameObjectsWithTag("Spawner");

        StartInvoke();
    }

    void StartInvoke()
    {
        InvokeRepeating(nameof(SpawnObjects), 2.0f, 5.0f);
    }

    // Update is called once per frame
    void SpawnObjects()
    {
        float listLength = spawnerList.Length;
        int randomIndex = (int)Random.Range(0.0f, listLength - 1.0f);
        GameObject randomSpawner = spawnerList[randomIndex];

        int powerupRoll = (int)Random.Range(0.0f, 10.0f);
        if (powerupRoll <= chanceOfPowerup)
        {
            int randomPowerup = (int)Random.Range(0.0f, powerupList.Length);
            Instantiate(powerupList[randomPowerup], randomSpawner.transform.position + new Vector3(0, 1.0f, 0), randomSpawner.transform.rotation);
        }
        else
        {
            Instantiate(enemyPrefab, randomSpawner.transform.position, randomSpawner.transform.rotation);
        }
    }

    public void FreezeSpawners(float freezeTime) 
    {
        CancelInvoke();
        StartCoroutine(PauseSpawners(freezeTime));
    }

    IEnumerator PauseSpawners(float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
        StartInvoke();
    }
}
