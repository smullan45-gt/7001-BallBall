using UnityEngine;

public class RushModeManager : MonoBehaviour
{
    public int pointMultiplier;
    public int enemySpeedMultiplier;
    public int enemyLaunchMultiplier;

    public float rushSpawnRate;

    public bool isRushActive;

    private EnemySpawner spawnManager;

    public static RushModeManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnManager = GameObject.Find("GameManager").GetComponent<EnemySpawner>();
        isRushActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateRushMode()
    {
        Debug.Log("RUSH MODE ACTIVATED!!!");

        isRushActive = true;

        spawnManager.CancelSpawnInvoke();
        spawnManager.StartInvoke(0.0f, rushSpawnRate);
    }
}
