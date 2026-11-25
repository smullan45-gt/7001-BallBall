using UnityEngine;

public class Spawner : MonoBehaviour
{
    bool hasPowerup = false;
    Vector3 powerupSpawnYOffset = new Vector3(0, 1.0f, 0);

    public void SetHasPowerup(bool toggle)
    {
        hasPowerup = toggle;
    }
    
    public bool GetHasPowerup()
    {
        return hasPowerup;
    }

    public void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, gameObject.transform.position, gameObject.transform.rotation);
    }

    public void SpawnPowerup(GameObject powerup)
    {
        Instantiate(powerup, gameObject.transform.position + powerupSpawnYOffset, gameObject.transform.rotation);
        SetHasPowerup(true);
    }
}
