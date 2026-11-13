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

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        TogglePhysics(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemyHit();
    }

    void TogglePhysics(bool isPhysics)
    {
        if(isPhysics == true)
        {
            Debug.Log("Disabling NavMesh, Enabling Rigidbody");
            agent.enabled = false;
            rb = gameObject.AddComponent<Rigidbody>() as Rigidbody;
        }
        else
        {
            Debug.Log("Disabling Rigidbody, Enabling NavMesh...");
            Destroy(rb);
            agent.enabled = true;
        }
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
        if(agent.enabled)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    public void HitEnemy()
    {
        Debug.Log("Setting isHit to True");
        isHit = true;
        TogglePhysics(true);
        StartCoroutine(StunEnemy());
    }

    IEnumerator StunEnemy()
    {
        Debug.Log("Stunning Enemy...");
        yield return new WaitForSeconds(stunTime);
        Debug.Log("Setting isHit to false");
        isHit = false;
        TogglePhysics(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Bounds")
        {
            Destroy(gameObject);
        }
    }
}
