using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PianoGameController : MonoBehaviour
{
    [SerializeField] private AudioSource[] pianoKeys;
    [SerializeField] private Button[] keyButtons;
    [SerializeField] private float keyDisplayTime = 0.5f;
    [SerializeField] private int melodyLength = 3;
    [SerializeField] private Color pressedColor = Color.black;
    [SerializeField] private Color incorrectColor = Color.red;
    [SerializeField] private float colorChangeDuration = 0.3f;
    [SerializeField] private float scaleMultiplier = 1.2f;
    [SerializeField] private float scaleDuration = 0.2f;

    private List<int> generatedMelody = new List<int>();
    private List<int> playerInput = new List<int>();
    private bool isPlayerTurn = false;
    private bool canPressKey = true;
    private int correctStreak = 0;
    private Dictionary<Button, Vector3> originalScales = new Dictionary<Button, Vector3>();
    private Dictionary<Button, Color> originalColors = new Dictionary<Button, Color>();

    private void Start()
    {
        foreach (Button button in keyButtons)
        {
            originalColors[button] = button.GetComponent<Image>().color;
            originalScales[button] = button.GetComponent<RectTransform>().localScale;
        }

        for (int i = 0; i < keyButtons.Length; i++)
        {
            int index = i;
            keyButtons[i].onClick.AddListener(() => PlayerPressKey(index));
        }

        GenerateMelody();
        StartCoroutine(PlayMelody());
    }

    private void GenerateMelody()
    {
        generatedMelody.Clear();
        for (int i = 0; i < melodyLength; i++)
        {
            generatedMelody.Add(Random.Range(0, pianoKeys.Length));
        }
    }

    private IEnumerator PlayMelody()
    {
        isPlayerTurn = false;
        yield return new WaitForSeconds(1f);

        foreach (int keyIndex in generatedMelody)
        {
            PlayKey(keyIndex);
            yield return new WaitForSeconds(keyDisplayTime);
        }

        isPlayerTurn = true;
    }

    private void PlayKey(int keyIndex)
    {
        pianoKeys[keyIndex].Play();
        StartCoroutine(ChangeButtonColor(keyButtons[keyIndex], pressedColor));
        StartCoroutine(ScaleButton(keyButtons[keyIndex]));
    }

    public void PlayerPressKey(int keyIndex)
    {
        if (!isPlayerTurn || !canPressKey) return;

        playerInput.Add(keyIndex);
        PlayKey(keyIndex);

        if (playerInput[playerInput.Count - 1] != generatedMelody[playerInput.Count - 1])
        {
            correctStreak = 0;
            StartCoroutine(HandleIncorrectKey(keyButtons[keyIndex]));
        }
        else if (playerInput.Count == generatedMelody.Count)
        {
            correctStreak++;
            StartCoroutine(PlayCorrectMelody());
        }
    }

    private IEnumerator PlayCorrectMelody()
    {
        isPlayerTurn = false;
        yield return new WaitForSeconds(1f);

        float fasterKeyDisplayTime = keyDisplayTime / 3f;
        foreach (int keyIndex in generatedMelody)
        {
            PlayKey(keyIndex);
            yield return new WaitForSeconds(fasterKeyDisplayTime);
        }

        playerInput.Clear();

        if (correctStreak >= 2)
        {
            melodyLength++;
        }

        GenerateMelody();
        StartCoroutine(PlayMelody());
    }

    private IEnumerator ChangeButtonColor(Button button, Color targetColor)
    {
        Image buttonImage = button.GetComponent<Image>();
        buttonImage.color = targetColor;

        yield return new WaitForSeconds(colorChangeDuration);

        buttonImage.color = originalColors[button];
    }

    private IEnumerator ScaleButton(Button button)
    {
        RectTransform buttonRect = button.GetComponent<RectTransform>();
        Vector3 originalScale = originalScales[button];
        Vector3 targetScale = originalScale * scaleMultiplier;

        float elapsedTime = 0;
        while (elapsedTime < scaleDuration)
        {
            buttonRect.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        buttonRect.localScale = targetScale;

        elapsedTime = 0;
        while (elapsedTime < scaleDuration)
        {
            buttonRect.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        buttonRect.localScale = originalScale;
    }

    private IEnumerator HandleIncorrectKey(Button button)
    {
        isPlayerTurn = false;
        StartCoroutine(ChangeButtonColor(button, incorrectColor));
        StartCoroutine(ScaleButton(button));
        yield return new WaitForSeconds(1f);

        playerInput.Clear();
        StartCoroutine(PlayMelody());
    }
}