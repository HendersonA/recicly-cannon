using System;
using UnityEngine;

public class TakeItem : MonoBehaviour
{
    public static Action<ItemScriptable> OnGetItem;
    [SerializeField] private ItemScriptable itemScriptable;

    private bool isCollide = false;

    private void DepositItem()
    {
        if (isCollide)
        {
            itemScriptable.itemCount += itemScriptable.itemCollectedCount;
            itemScriptable.itemCollectedCount = 0;
            OnGetItem?.Invoke(itemScriptable);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCollide = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCollide = false;
        }
    }

    void OnEnable()
    {
        JoystickButton.OnJoystickButtonCallback += DepositItem;
    }

    void OnDisable()
    {
        JoystickButton.OnJoystickButtonCallback -= DepositItem;
    }
}
