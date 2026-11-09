using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject player;

    private bool isHit = false;

    public float stunTime = 3.0f;

    private BoxCollider collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

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
        agent.enabled = false;
        StartCoroutine(StunEnemy());
    }

    IEnumerator StunEnemy()
    {
        Debug.Log("Stunning Enemy...");
        yield return new WaitForSeconds(stunTime);
        Debug.Log("Setting isHit to false");
        isHit = false;
        agent.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Bounds")
        {
            Destroy(gameObject);
        }
    }
}
