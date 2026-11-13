using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 20.0f;
    private bool isTimerRunning = false;
    private float currentTime;
    private ScoreSystem scoreCounter;

    void Start()
    {
        isTimerRunning = true;
        scoreCounter = GameObject.Find("GoalZone").GetComponent<ScoreSystem>();
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
                Debug.Log("TIME UP! Final Score: " + scoreCounter.currentScore);
                timeRemaining = 0.0f;
                isTimerRunning = false;
            }
        }
    }
}
