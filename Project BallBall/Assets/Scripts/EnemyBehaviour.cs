using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;

    private GameObject player;

    private bool isHit = false;

    private BoxCollider collider;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        TogglePhysics(false);

        player = GameObject.FindWithTag("Player");
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
            agent.enabled = false;
            rb = gameObject.AddComponent<Rigidbody>();
        }
        else
        {
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

    public void HitEnemy(float stun)
    {
        if(isHit == false)
        {
            isHit = true;
            TogglePhysics(true);
            StartCoroutine(StunEnemy(stun));
        }
    }

    IEnumerator StunEnemy(float stun)
    {
        Debug.Log("Stunning Enemy...");
        yield return new WaitForSeconds(stun);
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
