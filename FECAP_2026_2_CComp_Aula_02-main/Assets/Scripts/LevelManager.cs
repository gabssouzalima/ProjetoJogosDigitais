using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    float enemySpawnTimer = 5f;
    float enemySpawnCooldown = 0f;
    public int enemiesPerSpawn = 2;
    public float difficultyTimer = 30f; 
    float difficultyCooldown = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        enemySpawnCooldown += Time.deltaTime;
        if (enemySpawnCooldown >= enemySpawnTimer)
        {
            SpawnEnemy(enemiesPerSpawn); 
            enemySpawnCooldown = 0f; 
        }
    }

    void SpawnEnemy(int amountToSpawn)
    {
        float posX = Random.Range(-20f, 20f);
        float posZ = Random.Range(-20f, 20f);

        Vector3 pos = new Vector3(posX, 1.5f, posZ);

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}
