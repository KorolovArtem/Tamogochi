using UnityEngine;

public class ActionObject : MonoBehaviour
{
    [SerializeField] private string actionType;

    private void OnMouseDown()
    {
        if (!string.IsNullOrEmpty(actionType) && SlimeActions.Instance != null)
        {
            SlimeActions.Instance.CompleteAction(actionType);
        }
    }
}
