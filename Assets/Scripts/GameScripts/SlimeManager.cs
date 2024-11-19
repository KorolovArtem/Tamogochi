using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlimeManager : MonoBehaviour
{
    [SerializeField] private InputField nameInputField;
    [SerializeField] private Slider hueSlider;
    [SerializeField] private Button startButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button languageButton;
    [SerializeField] private Sprite englishFlagSprite;
    [SerializeField] private Sprite russianFlagSprite;
    [SerializeField] private Text errorMessage;
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private Button confirmYesButton;
    [SerializeField] private Button confirmNoButton;

    private string currentLanguage;
    private bool isSettingsPanelVisible = false;

    private void Start()
    {
        LoadData();

        startButton.onClick.AddListener(ValidateAndStartGame);
        settingsButton.onClick.AddListener(ToggleSettingsPanel);
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
        languageButton.onClick.AddListener(ToggleLanguage);
        resetButton.onClick.AddListener(ResetDataPrompt);
        confirmYesButton.onClick.AddListener(ResetData);
        confirmNoButton.onClick.AddListener(HideConfirmationPanel);
    }

    private void ValidateAndStartGame()
    {
        string slimeName = nameInputField.text;
        if (IsValidName(slimeName))
        {
            SlimeStats.Instance.SetSlimeName(slimeName);
            SlimeStats.Instance.SetHue(hueSlider.value);

            SceneManager.LoadScene("HouseScene");
        }
        else
        {
            errorMessage.text = "Invalid name! Must be 1-12 characters.";
            errorMessage.gameObject.SetActive(true);
        }
    }

    private bool IsValidName(string name)
    {
        return name.Length > 0 && name.Length <= 12;
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("SlimeName"))
        {
            nameInputField.text = PlayerPrefs.GetString("SlimeName");
            hueSlider.value = PlayerPrefs.GetFloat("SlimeHue");
            nameInputField.interactable = false;
            hueSlider.interactable = false;
        }
    }

    private void ResetDataPrompt()
    {
        confirmationPanel.SetActive(true);
    }

    private void ResetData()
    {
        PlayerPrefs.DeleteAll();
        nameInputField.text = "";
        hueSlider.value = 0;
        nameInputField.interactable = true;
        hueSlider.interactable = true;
        HideConfirmationPanel();
    }

    private void ToggleSettingsPanel()
    {
        isSettingsPanelVisible = !isSettingsPanelVisible;
        settingsPanel.SetActive(isSettingsPanelVisible);
    }

    private void HideConfirmationPanel()
    {
        confirmationPanel.SetActive(false);
    }

    private void ChangeVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    private void ToggleLanguage()
    {
        if (currentLanguage == "English")
        {
            currentLanguage = "Russian";
            languageButton.image.sprite = russianFlagSprite;
        }
        else
        {
            currentLanguage = "English";
            languageButton.image.sprite = englishFlagSprite;
        }
        PlayerPrefs.SetString("Language", currentLanguage);
    }
}
