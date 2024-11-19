using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ArcadeMenuManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string arcadeMenuSceneName = "ArcadeMenu";

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(arcadeMenuSceneName);
    }
}