using UnityEngine;

public class RandomRadiusMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float radius = 5.0f;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = GetRandomTarget();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            targetPosition = GetRandomTarget();
        }
    }

    private Vector3 GetRandomTarget()
    {
        Vector2 randomPoint = Random.insideUnitCircle * radius;

        return new Vector3(startPosition.x + randomPoint.x, startPosition.y, startPosition.z + randomPoint.y);
    }
}
