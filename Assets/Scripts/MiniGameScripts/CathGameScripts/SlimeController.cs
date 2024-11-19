using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float minX = -5.0f;
    [SerializeField] private float maxX = 5.0f;
    public GameObject restartButton;
    private FoodSpawner foodSpawner;
    private bool isGameOver = false;

    void Start()
    {
        restartButton.SetActive(false);
        foodSpawner = FindObjectOfType<FoodSpawner>();
    }

    private void Update()
    {
        if (!isGameOver)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 newPosition = transform.position + Vector3.right * horizontalInput * moveSpeed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            transform.position = newPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BadObject"))
        {
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            isGameOver = true;
            foodSpawner.StopSpawning();
            ShowRestartButton();
        }

        if (other.CompareTag("Food"))
        {
            Debug.Log("Food Caught!");
            Destroy(other.gameObject);
        }
    }

    private void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}