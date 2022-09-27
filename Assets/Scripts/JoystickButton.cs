using UnityEngine;
using UnityEngine.UI;
using System;

public class JoystickButton : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button buttonCollect;

    public static event Action OnJoystickButtonCallback;

    void Start()
    {
        buttonCollect.onClick.AddListener(() =>
        {
            OnJoystickButtonCallback?.Invoke();
        });
    }
}
