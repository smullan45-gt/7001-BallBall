using UnityEngine;
using System.Collections;

public class PowerupBehaviour : MonoBehaviour
{
    public float freezeTime = 5.0f;
    public float growTime = 5.0f;
    public float growthScale = 2.0f;

    [SerializeField] private float rayDistance = 5f;

    private bool isGrown = false;

    private EnemySpawner spawnManager;
    private Spawner originSpawner;

    private GameObject[] enemyList;
    private GameObject[] goalPostList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnManager = GameObject.Find("GameManager").GetComponent<EnemySpawner>();
        goalPostList = GameObject.FindGameObjectsWithTag("Posts");

        FindOriginSpawner();
    }

    void FindOriginSpawner()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, rayDistance))
        {
            Debug.Log(hit.collider.name);

            if(hit.transform.gameObject.tag == "Spawner")
            {
                originSpawner = hit.transform.gameObject.GetComponent<Spawner>();
            }
            else
            {
                Debug.Log("Weh! No Spawner???");
            }
        }
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
        DestroyPowerup(gameObject);
        spawnManager.FreezeSpawners(freezeTime);
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
        gameObject.transform.Translate(new Vector3(0.0f, -10.0f, 0.0f)); //powerup cannot be destroyed until coroutine is done
        if (isGrown == false)
        {
            foreach (GameObject goalPost in goalPostList)
            {
                goalPost.transform.localScale *= growthScale;
            }

            isGrown = true;
            StartCoroutine(PauseBeforeShrink(growTime));
        }
        
    }

    IEnumerator PauseBeforeShrink(float growTime) 
    {
        yield return new WaitForSeconds(growTime);
        Debug.Log("Should start foreach...");
        foreach (GameObject goalPost in goalPostList)
        {
            Debug.Log("Shrinking Goals");
            goalPost.transform.localScale /= growthScale;
        }

        isGrown = false;
        DestroyPowerup(gameObject);
    }

    void DestroyPowerup(GameObject gameObject)
    {
        Destroy(gameObject);
        originSpawner.SetHasPowerup(false);
    }
}
