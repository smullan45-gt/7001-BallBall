using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bat")
        {
            Debug.Log("THWACK!");
            gameObject.GetComponent<EnemyBehaviour>().HitEnemy();
            rb.AddForce(transform.up * 10.0f, ForceMode.Impulse);
            
        }
    }
}
