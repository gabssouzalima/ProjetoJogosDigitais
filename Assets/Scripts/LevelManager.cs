using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Spawn")]
    public float enemySpawnTimer = 5f;     // intervalo entre ondas (segundos)
    public int enemiesPerSpawn = 2;        // quantos zumbis nascem por onda
    public int maxEnemiesPerSpawn = 10;    // limite para a dificuldade não crescer sem parar
    public Transform arenaCenter;          
    public float spawnRadius = 20f;        
    public float spawnHeight = 1.5f;       // altura acima do chão

    [Header("Dificuldade")]
    public float difficultyTimer = 30f;    // a cada quantos segundos aumenta a quantidade

    float enemySpawnCooldown = 0f;
    float difficultyCooldown = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Cronômetro de spawn
        enemySpawnCooldown += Time.deltaTime;
        if (enemySpawnCooldown >= enemySpawnTimer)
        {
            SpawnEnemy(enemiesPerSpawn);
            enemySpawnCooldown = 0f;
        }

        // Cronômetro de dificuldade
        difficultyCooldown += Time.deltaTime;
        if (difficultyCooldown >= difficultyTimer)
        {
            difficultyCooldown = 0f;
            if (enemiesPerSpawn < maxEnemiesPerSpawn)
                enemiesPerSpawn++;
        }
    }

    void SpawnEnemy(int amountToSpawn)
    {
        // se não arrastar o centro, usa o ponto (0, 0, 0) do mundo
        Vector3 center = arenaCenter != null ? arenaCenter.position : Vector3.zero;

        for (int i = 0; i < amountToSpawn; i++)
        {
            // posição aleatória dentro de um circulo, 
            Vector2 circle = Random.insideUnitCircle * spawnRadius;

            Vector3 pos = new Vector3(
                center.x + circle.x,
                center.y + spawnHeight,
                center.z + circle.y
            );

            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
}