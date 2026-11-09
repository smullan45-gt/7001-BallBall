using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 20.0f;

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            Debug.Log("Time Remaining: " + timeRemaining);
        }
        else
        {
            Debug.Log("TIME UP!");
        }
    }
}
