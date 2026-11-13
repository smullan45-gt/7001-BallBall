using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 20.0f;
    private bool isTimerRunning = false;
    private float currentTime;

    void Start()
    {
        isTimerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimerRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                float seconds = Mathf.FloorToInt(timeRemaining % 60);

                if(seconds != currentTime) Debug.Log("Time Remaining: " + seconds);

                currentTime = seconds;
            }
            else
            {
                Debug.Log("TIME UP!");
                timeRemaining = 0.0f;
                isTimerRunning = false;
            }
        }
    }
}
