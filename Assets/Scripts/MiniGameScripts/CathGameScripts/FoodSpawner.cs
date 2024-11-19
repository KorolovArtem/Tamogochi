using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] foodPrefabs;
    [SerializeField] private GameObject[] badObjects;
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;
    [SerializeField] private Transform targetZone;
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private float minImpulseForce = 5f;
    [SerializeField] private float maxImpulseForce = 10f;

    private bool isSpawning = true;

    private void Start()
    {
        InvokeRepeating("SpawnFood", spawnInterval, spawnInterval);
    }

    private void SpawnFood()
    {
        if (!isSpawning) return;

        bool spawnLeft = Random.Range(0, 2) == 0;
        Vector3 spawnPos = spawnLeft ? leftSpawnPoint.position : rightSpawnPoint.position;

        GameObject food = Random.Range(0, 5) == 0 ? badObjects[Random.Range(0, badObjects.Length)] : foodPrefabs[Random.Range(0, foodPrefabs.Length)];

        GameObject spawnedObject = Instantiate(food, spawnPos, food.transform.rotation);

        Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 directionToTarget = (targetZone.position - spawnPos).normalized;

            float randomForce = Random.Range(minImpulseForce, maxImpulseForce);
            rb.AddForce(directionToTarget * randomForce, ForceMode.Impulse);
            Vector3 randomTorque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rb.AddTorque(randomTorque * randomForce, ForceMode.Impulse);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke("SpawnFood");
    }
}