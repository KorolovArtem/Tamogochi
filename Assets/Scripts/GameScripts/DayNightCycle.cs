using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private Light[] houseLights;
    [SerializeField] private float dayDuration = 120f;
    [SerializeField] private float eveningDuration = 60f;
    [SerializeField] private float nightDuration = 120f;
    [SerializeField] private Vector3 dayRotation;
    [SerializeField] private Vector3 eveningRotation;
    [SerializeField] private Vector3 nightRotation;
    [SerializeField] private float transitionDuration = 2f;
    private Quaternion targetRotation;
    private float transitionProgress = 0f;

    private enum DayPhase { Day, Evening, Night }
    private DayPhase currentPhase;
    private float cycleTimer;

    private void Start()
    {
        currentPhase = DayPhase.Day;
        cycleTimer = dayDuration;
        UpdateLighting();
    }

    private void Update()
    {
        cycleTimer -= Time.deltaTime;

        if (transitionProgress < 1f)
        {
            transitionProgress += Time.deltaTime / transitionDuration;
            directionalLight.transform.rotation = Quaternion.Lerp(directionalLight.transform.rotation, targetRotation, transitionProgress);
        }

        if (cycleTimer <= 0)
        {
            SwitchToNextPhase();
        }
    }


    private void SwitchToNextPhase()
    {
        switch (currentPhase)
        {
            case DayPhase.Day:
                currentPhase = DayPhase.Evening;
                cycleTimer = eveningDuration;
                UpdateLighting();
                break;
            case DayPhase.Evening:
                currentPhase = DayPhase.Night;
                cycleTimer = nightDuration;
                EnableHouseLights(true);
                UpdateLighting();
                break;
            case DayPhase.Night:
                currentPhase = DayPhase.Day;
                cycleTimer = dayDuration;
                EnableHouseLights(false);
                UpdateLighting();
                break;
        }
    }
    private void EnableHouseLights(bool enable)
    {
        foreach (Light houseLight in houseLights)
        {
            houseLight.enabled = enable;
        }
    }
    private void UpdateLighting()
    {
        switch (currentPhase)
        {
            case DayPhase.Day:
                targetRotation = Quaternion.Euler(dayRotation);
                break;
            case DayPhase.Evening:
                targetRotation = Quaternion.Euler(eveningRotation);
                break;
            case DayPhase.Night:
                targetRotation = Quaternion.Euler(nightRotation);
                break;
        }

        transitionProgress = 0f;
    }
}
