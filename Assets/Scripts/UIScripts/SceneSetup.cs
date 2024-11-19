using UnityEngine;
using UnityEngine.UI;

public class SceneSetup : MonoBehaviour
{
    [SerializeField] private Text slimeNameText;
    [SerializeField] private Text slimeLevelText;
    [SerializeField] private SkinnedMeshRenderer slimeRenderer;

    private void Start()
    {
        if (SlimeStats.Instance != null)
        {
            SlimeStats.Instance.UpdateUIElements(slimeNameText, slimeLevelText, slimeRenderer);
        }
    }
}