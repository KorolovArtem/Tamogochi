using UnityEngine;
using UnityEngine.UI;
using System;

public class SlimeStats : MonoBehaviour
{
    public static SlimeStats Instance { get; private set; }

    public string SlimeName { get; private set; }
    public int Level { get; private set; }
    public float currentExperience = 0f;
    public float experienceToNextLevel = 100f;

    [Header("UI Elements")]
    [SerializeField] private Text slimeNameText;
    [SerializeField] private Text slimeLevelText;
    [SerializeField] private SkinnedMeshRenderer slimeRenderer;

    public float Hue { get; private set; }
    public float Hunger { get; private set; } = 100f;
    public float Mood { get; private set; } = 100f;
    public float Energy { get; private set; } = 100f;
    public float Health { get; private set; } = 100f;

    private const float maxStatValue = 100f;

    public event Action<float> OnLevelProgressChanged;
    public event Action<int> OnLevelChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
        UpdateSlimeColor();
    }

    public void SetSlimeName(string name)
    {
        SlimeName = name;
        PlayerPrefs.SetString("SlimeName", name);
        PlayerPrefs.Save();
        UpdateUI();
    }

    public void SetHue(float hue)
    {
        Hue = hue;
        PlayerPrefs.SetFloat("SlimeHue", Hue);
        PlayerPrefs.Save();
        UpdateSlimeColor();
    }

    private void UpdateSlimeColor()
    {
        if (slimeRenderer != null)
        {
            Color slimeColor = Color.HSVToRGB(Hue, 1, 1);
            slimeRenderer.material.color = slimeColor;
        }
    }

    public void AddExperience(float amount)
    {
        currentExperience += amount;
        PlayerPrefs.SetFloat("CurrentExperience", currentExperience);
        PlayerPrefs.Save();

        if (currentExperience >= experienceToNextLevel)
        {
            LevelUp();
        }
        UpdateExperienceUI();
    }

    private void LevelUp()
    {
        Level++;
        currentExperience -= experienceToNextLevel;
        PlayerPrefs.SetInt("SlimeLevel", Level);
        PlayerPrefs.Save();
        OnLevelChanged?.Invoke(Level);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (slimeNameText != null)
        {
            slimeNameText.text = SlimeName;
        }
        if (slimeLevelText != null)
        {
            slimeLevelText.text = "Level: " + Level;
        }
    }

    private void UpdateExperienceUI()
    {
        float progress = currentExperience / experienceToNextLevel;
        OnLevelProgressChanged?.Invoke(progress);
    }

    public void LoadData()
    {
        SlimeName = PlayerPrefs.GetString("SlimeName", "Default Slime");
        Level = PlayerPrefs.GetInt("SlimeLevel", 1);
        currentExperience = PlayerPrefs.GetFloat("CurrentExperience", 0);
        Hunger = PlayerPrefs.GetFloat("SlimeHunger", maxStatValue);
        Mood = PlayerPrefs.GetFloat("SlimeMood", maxStatValue);
        Energy = PlayerPrefs.GetFloat("SlimeEnergy", maxStatValue);
        Health = PlayerPrefs.GetFloat("SlimeHealth", maxStatValue);
        Hue = PlayerPrefs.GetFloat("SlimeHue", 0.5f);

        UpdateUI();
        UpdateSlimeColor();
    }

    public void ModifyHunger(float amount)
    {
        Hunger = Mathf.Clamp(Hunger + amount, 0, maxStatValue);
        PlayerPrefs.SetFloat("SlimeHunger", Hunger);
        PlayerPrefs.Save();
    }

    public void ModifyMood(float amount)
    {
        Mood = Mathf.Clamp(Mood + amount, 0, maxStatValue);
        PlayerPrefs.SetFloat("SlimeMood", Mood);
        PlayerPrefs.Save();
    }

    public void ModifyEnergy(float amount)
    {
        Energy = Mathf.Clamp(Energy + amount, 0, maxStatValue);
        PlayerPrefs.SetFloat("SlimeEnergy", Energy);
        PlayerPrefs.Save();
    }

    public void ModifyHealth(float amount)
    {
        Health = Mathf.Clamp(Health + amount, 0, maxStatValue);
        PlayerPrefs.SetFloat("SlimeHealth", Health);
        PlayerPrefs.Save();
    }

    public void UpdateUIElements(Text nameText, Text levelText, SkinnedMeshRenderer renderer)
    {
        slimeNameText = nameText;
        slimeLevelText = levelText;
        slimeRenderer = renderer;
        UpdateUI();
        UpdateSlimeColor();
    }
}