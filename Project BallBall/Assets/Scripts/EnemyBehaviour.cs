using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject player;

    public bool isHit = false;

    public float stunTime = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        CheckEnemyHit();
    }

    void CheckEnemyHit()
    {
        if(!isHit)
        {
            MoveEnemyToPlayer();
        }
    }

    void MoveEnemyToPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    public void HitEnemy()
    {
        Debug.Log("Setting isHit to True");
        isHit = true;
        StartCoroutine(StunEnemy());
    }

    IEnumerator StunEnemy()
    {
        Debug.Log("Stunning Enemy...");
        yield return new WaitForSeconds(stunTime);
        Debug.Log("Setting isHit to false");
        isHit = false;
    }
}
