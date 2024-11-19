using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] private string houseSceneName = "HouseScene";
    [SerializeField] private string runGameSceneName = "RunGame";
    [SerializeField] private string memoryGameSceneName = "MemoryGame";
    [SerializeField] private string catchGameSceneName = "CatchGame";

    public void OnButtonHouseClick()
    {
        SceneManager.LoadScene(houseSceneName);
    }
    public void OnButtonRunGameClick()
    {
        SceneManager.LoadScene(runGameSceneName);
    }
    public void OnButtonMemoryGameClick()
    {
        SceneManager.LoadScene(memoryGameSceneName);
    }
    public void OnButtonCatchGameClick()
    {
        SceneManager.LoadScene(catchGameSceneName);
    }
}