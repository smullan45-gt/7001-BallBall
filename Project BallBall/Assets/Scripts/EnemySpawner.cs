using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject[] spawnerList;
    public GameObject enemyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnerList = GameObject.FindGameObjectsWithTag("Spawner");

        InvokeRepeating(nameof(SpawnEnemies), 2.0f, 5.0f);
    }

    // Update is called once per frame
    void SpawnEnemies()
    {
        float listLength = spawnerList.Length;
        int randomIndex = (int)Random.Range(0.0f, listLength - 1.0f);
        GameObject randomSpawner = spawnerList[randomIndex];

        Instantiate(enemyPrefab, randomSpawner.transform.position, randomSpawner.transform.rotation);
    }
}
