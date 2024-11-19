using UnityEngine;
using UnityEngine.UI;

public class SlimeStatsUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image[] hungerDivisions;
    [SerializeField] private Image hungerIcon;

    [SerializeField] private Image[] moodDivisions;
    [SerializeField] private Image moodIcon;

    [SerializeField] private Image[] energyDivisions;
    [SerializeField] private Image energyIcon;

    [SerializeField] private Image[] healthDivisions;
    [SerializeField] private Image healthIcon;

    private Color[] hungerOriginalColors;
    private Color[] moodOriginalColors;
    private Color[] energyOriginalColors;
    private Color[] healthOriginalColors;

    private void Start()
    {
        hungerOriginalColors = SaveOriginalColors(hungerDivisions);
        moodOriginalColors = SaveOriginalColors(moodDivisions);
        energyOriginalColors = SaveOriginalColors(energyDivisions);
        healthOriginalColors = SaveOriginalColors(healthDivisions);
    }

    private void Update()
    {
        if (SlimeStats.Instance != null)
        {
            UpdateStat(SlimeStats.Instance.Hunger, hungerDivisions, hungerIcon, hungerOriginalColors);
            UpdateStat(SlimeStats.Instance.Mood, moodDivisions, moodIcon, moodOriginalColors);
            UpdateStat(SlimeStats.Instance.Energy, energyDivisions, energyIcon, energyOriginalColors);
            UpdateStat(SlimeStats.Instance.Health, healthDivisions, healthIcon, healthOriginalColors);
        }
    }

    private void UpdateStat(float value, Image[] divisions, Image icon, Color[] originalColors)
    {
        if (divisions != null && icon != null)
        {
            int filledDivisions = Mathf.CeilToInt(value / 33.3f);

            for (int i = 0; i < divisions.Length; i++)
            {
                if (divisions[i] != null)
                {
                    Color newColor = originalColors[i];
                    newColor.a = i < filledDivisions ? 1f : 0f;
                    divisions[i].color = newColor;
                    divisions[i].enabled = i < filledDivisions;
                }
            }

            icon.enabled = filledDivisions > 0;
        }
    }

    private Color[] SaveOriginalColors(Image[] divisions)
    {
        Color[] originalColors = new Color[divisions.Length];
        for (int i = 0; i < divisions.Length; i++)
        {
            if (divisions[i] != null)
            {
                originalColors[i] = divisions[i].color;
            }
        }
        return originalColors;
    }
}
