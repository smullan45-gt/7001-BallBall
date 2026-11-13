using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public float batForce = 100.0f;
    public float launchAngleY = 3.0f;

    private GameObject character;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character = GameObject.Find("Face");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy" && gameObject.tag == "Bat")
        {
            Debug.Log("THWACK!");
            other.gameObject.GetComponent<EnemyBehaviour>().HitEnemy();
            Vector3 playerDirection = character.transform.forward;
            playerDirection.y = launchAngleY;
            other.gameObject.GetComponent<Rigidbody>().AddForce(playerDirection * batForce, ForceMode.Impulse);  
        }
    }
}
