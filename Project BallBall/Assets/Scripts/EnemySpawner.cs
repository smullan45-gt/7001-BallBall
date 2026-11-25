using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    private List<Spawner> spawnerList = new List<Spawner>();

    public GameObject[] powerupList;
    public GameObject enemyPrefab;

    private int chanceOfPowerup = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindSpawners();

        StartInvoke();
    }

    void FindSpawners()
    {
        GameObject[] objectList = GameObject.FindGameObjectsWithTag("Spawner");

        foreach (GameObject gObject in objectList)
        {
            Spawner spawner = gObject.GetComponent<Spawner>();
            spawnerList.Add(spawner);
        }
    }

    void StartInvoke()
    {
        InvokeRepeating(nameof(SpawnObjects), 2.0f, 5.0f);
    }

    // Update is called once per frame
    void SpawnObjects()
    {
        float listLength = spawnerList.Count;
        int randomIndex = (int)Random.Range(0.0f, listLength);
        Spawner randomSpawner = spawnerList[randomIndex];

        int powerupRoll = (int)Random.Range(0.0f, 10.0f);

        Debug.Log("Spawning at no. " + randomIndex);
        if (powerupRoll <= chanceOfPowerup && randomSpawner.GetHasPowerup() == false)
        {
            int randomPowerup = (int)Random.Range(0.0f, powerupList.Length);
            randomSpawner.SpawnPowerup(powerupList[randomPowerup]);
        }
        else
        {
            randomSpawner.SpawnEnemy(enemyPrefab);
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
