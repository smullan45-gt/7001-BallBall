using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public float launchForce = 10.0f;
    public float playerMass = 3.0f;

    public Vector3 impact = Vector3.zero;

    private CharacterController charCon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charCon = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(impact.magnitude > 0.2f)
        {
            charCon.Move(impact * Time.deltaTime);
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        }
    }

    void AddImpact(Vector3 launchDirection, float lForce)
    {
        launchDirection.Normalize();
        if(launchDirection.y < 0)
        {
            launchDirection.y = -launchDirection.y;
        }
        impact += launchDirection * (lForce / playerMass);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Player Hit!");
            Vector3 launchDirection = other.gameObject.transform.forward;
            AddImpact(launchDirection, launchForce);
        }
    }
}
