using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    private List<Spawner> spawnerList = new List<Spawner>();

    public GameObject[] powerupList;
    public GameObject enemyPrefab;

    [field: SerializeField] private float startup {get; set;}
    [field: SerializeField] private float interval {get; set;}

    private int chanceOfPowerup = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindSpawners();

        StartInvoke(startup, interval);
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

    public void StartInvoke(float startup, float interval)
    {
        InvokeRepeating(nameof(SpawnObjects), startup, interval);
    }

    public void CancelSpawnInvoke()
    {
        CancelInvoke();
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
        CancelSpawnInvoke();
        StartCoroutine(PauseSpawners(freezeTime));
    }

    IEnumerator PauseSpawners(float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
        StartInvoke(startup, interval);
    }
}
