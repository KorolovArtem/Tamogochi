using UnityEngine;

public class HouseRoomManager : MonoBehaviour
{
    [SerializeField] private Transform tamagotchi;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float tamagotchiMoveSpeed = 5f;
    [SerializeField] private float cameraMoveSpeed = 3f;

    [SerializeField] private Vector3 kitchenPosition;
    [SerializeField] private Vector3 playroomPosition;
    [SerializeField] private Vector3 bathPosition;
    [SerializeField] private Vector3 bedroomPosition;

    [SerializeField] private Vector3 cameraKitchenPosition;
    [SerializeField] private Vector3 cameraPlayroomPosition;
    [SerializeField] private Vector3 cameraBathPosition;
    [SerializeField] private Vector3 cameraBedroomPosition;

    [SerializeField] private Vector3 cameraKitchenRotation;
    [SerializeField] private Vector3 cameraPlayroomRotation;
    [SerializeField] private Vector3 cameraBathRotation;
    [SerializeField] private Vector3 cameraBedroomRotation;

    private Vector3 targetPosition;
    private Vector3 targetCameraPosition;
    private Vector3 targetCameraRotation;

    void Start()
    {
        targetPosition = tamagotchi.position;
        targetCameraPosition = cameraTransform.position;
        targetCameraRotation = cameraTransform.eulerAngles;
    }

    void Update()
    {
        tamagotchi.position = Vector3.MoveTowards(tamagotchi.position, targetPosition, tamagotchiMoveSpeed * Time.deltaTime);
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, Time.deltaTime * cameraMoveSpeed);
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, Quaternion.Euler(targetCameraRotation), Time.deltaTime * cameraMoveSpeed);
    }

    public void MoveToKitchen()
    {
        targetPosition = kitchenPosition;
        targetCameraPosition = cameraKitchenPosition;
        targetCameraRotation = cameraKitchenRotation;
    }

    public void MoveToPlayroom()
    {
        targetPosition = playroomPosition;
        targetCameraPosition = cameraPlayroomPosition;
        targetCameraRotation = cameraPlayroomRotation;
    }

    public void MoveToBath()
    {
        targetPosition = bathPosition;
        targetCameraPosition = cameraBathPosition;
        targetCameraRotation = cameraBathRotation;
    }

    public void MoveToBedroom()
    {
        targetPosition = bedroomPosition;
        targetCameraPosition = cameraBedroomPosition;
        targetCameraRotation = cameraBedroomRotation;
    }
}