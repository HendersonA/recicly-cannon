using System;
using UnityEngine;

public class TrashItem : MonoBehaviour
{
    [SerializeField] private ItemScriptable itemScriptable;

    private bool isCollide = false;

    private void TakeItem()
    {
        if (isCollide)
        {
            itemScriptable.itemCollectedCount++;
            Destroy(this.gameObject);
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
        JoystickButton.OnJoystickButtonCallback += TakeItem;
    }

    void OnDisable()
    {
        JoystickButton.OnJoystickButtonCallback -= TakeItem;
    }
}
