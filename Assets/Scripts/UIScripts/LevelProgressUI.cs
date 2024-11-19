using UnityEngine;
using UnityEngine.UI;

public class LevelProgressUI : MonoBehaviour
{
    [SerializeField] private Image experienceBarFill;

    private void Start()
    {
        if (SlimeStats.Instance != null)
        {
            SlimeStats.Instance.OnLevelProgressChanged += UpdateExperienceBar;
        }
    }

    private void OnDestroy()
    {
        if (SlimeStats.Instance != null)
        {
            SlimeStats.Instance.OnLevelProgressChanged -= UpdateExperienceBar;
        }
    }

    private void UpdateExperienceBar(float progress)
    {
        experienceBarFill.fillAmount = progress;
    }
}