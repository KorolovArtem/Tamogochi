using UnityEngine;

public class SlimeActions : MonoBehaviour
{
    public static SlimeActions Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompleteAction(string actionType)
    {
        if (SlimeStats.Instance == null) return;

        float experienceGain = 10f;

        switch (actionType)
        {
            case "Eat":
                SlimeStats.Instance.AddExperience(experienceGain);
                SlimeStats.Instance.ModifyHunger(20f);
                break;
            case "Play":
                SlimeStats.Instance.AddExperience(experienceGain);
                SlimeStats.Instance.ModifyMood(15f);
                break;
            case "Sleep":
                SlimeStats.Instance.AddExperience(experienceGain);
                SlimeStats.Instance.ModifyEnergy(25f);
                break;
            case "Heal":
                SlimeStats.Instance.AddExperience(experienceGain);
                SlimeStats.Instance.ModifyHealth(20f);
                break;
            default:
                break;
        }
    }
}