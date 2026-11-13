using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public float currentScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            currentScore++;
            Debug.Log("SCORE!!! Current Score: " + currentScore);
        }
    }
}
