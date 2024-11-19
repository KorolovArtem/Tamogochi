using UnityEngine;
using UnityEngine.UI;

public class ColourPickerControl : MonoBehaviour
{
    public float currentHue;

    [SerializeField] private RawImage hueImage;
    [SerializeField] private Slider hueSlider;
    [SerializeField] SkinnedMeshRenderer changeThis;

    private Texture2D hueTexture;

    private void Start()
    {
        CreateHueImage();

        if (PlayerPrefs.HasKey("SlimeHue"))
        {
            currentHue = PlayerPrefs.GetFloat("SlimeHue");
        }
        else
        {
            currentHue = 0.5f;
        }

        hueSlider.value = currentHue;

        UpdateSlimeColor();

        hueSlider.onValueChanged.AddListener(delegate { UpdateSlimeColor(); });
    }

    private void CreateHueImage()
    {
        hueTexture = new Texture2D(1, 16);
        hueTexture.wrapMode = TextureWrapMode.Clamp;
        hueTexture.name = "HueTexture";

        for (int i = 0; i < hueTexture.height; i++)
        {
            hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / hueTexture.height, 1, 1));
        }

        hueTexture.Apply();

        hueImage.texture = hueTexture;
    }

    public void UpdateSlimeColor()
    {
        currentHue = hueSlider.value;
        Color currentColour = Color.HSVToRGB(currentHue, 1, 1);
        changeThis.material.color = currentColour;
    }

    public void SaveSlimeColor()
    {
        PlayerPrefs.SetFloat("SlimeHue", currentHue);
        PlayerPrefs.Save();
    }
}
