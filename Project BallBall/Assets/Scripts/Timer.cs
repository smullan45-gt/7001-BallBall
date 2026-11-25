using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 20.0f;
    public float rushStartTime = 10.0f;
    private float currentTime;

    private bool isTimerRunning = false;

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
            else if(timeRemaining == rushStartTime)
            {
                RushModeManager.Instance.ActivateRushMode();
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
