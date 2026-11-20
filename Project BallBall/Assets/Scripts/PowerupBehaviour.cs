using UnityEngine;

public class PowerupBehaviour : MonoBehaviour
{
    public float freezeTime = 5.0f;

    private EnemySpawner spawner;

    private GameObject[] enemyList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner = GameObject.Find("GameManager").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag == "Ice") 
        {
            ActivateIcePowerup();
        }
    }

    void ActivateIcePowerup()
    {
        Debug.Log("Freeze!");
        Destroy(gameObject);
        spawner.FreezeSpawners(freezeTime);
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        GroupFreezeEnemies(enemyList);
    }

    void GroupFreezeEnemies(GameObject[] enemyList)
    {
        foreach (GameObject enemy in enemyList)
        {
            enemy.GetComponent<EnemyBehaviour>().HitEnemy(freezeTime);
        }
    }
}
