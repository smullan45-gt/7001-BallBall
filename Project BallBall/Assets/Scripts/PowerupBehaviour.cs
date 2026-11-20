using UnityEngine;
using System.Collections;

public class PowerupBehaviour : MonoBehaviour
{
    public float freezeTime = 5.0f;
    public float growTime = 5.0f;
    public float growthScale = 2.0f;

    private EnemySpawner spawner;

    private GameObject[] enemyList;
    private GameObject[] goalPostList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner = GameObject.Find("GameManager").GetComponent<EnemySpawner>();
        goalPostList = GameObject.FindGameObjectsWithTag("Posts");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Ice")
            {
                ActivateIcePowerup();
            }
            else if (gameObject.tag == "Grow")
            {
                ActivateGrowPowerup();
            }
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

    void ActivateGrowPowerup()
    {
        foreach (GameObject goalPost in goalPostList)
        {
            goalPost.transform.localScale *= growthScale;
        }

        StartCoroutine(PauseBeforeShrink());
    }

    IEnumerator PauseBeforeShrink() 
    {
        yield return new WaitForSeconds(growTime);

        foreach (GameObject goalPost in goalPostList)
        {
            goalPost.transform.localScale /= growthScale;
        }
    }
}
