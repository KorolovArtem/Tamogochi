using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Vector3 spawnPos = new Vector3(20, 4, -9);
    private float minSpawnDelay = 2.5f;
    private float maxSpawnDelay = 5f;
    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnObstacle();
    }

    void SpawnObstacle()
    {
        if (!playerControllerScript.gameOver)
        {
            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[randomIndex], spawnPos, obstaclePrefabs[randomIndex].transform.rotation);

            float randomDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            Invoke("SpawnObstacle", randomDelay);
        }
    }
}
