using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private FixedJoystick fixedJoystick;
    private Movement basicMovement;

    void OnEnable()
    {
        basicMovement = GetComponent<Movement>();
    }

    void Update()
    {
        Joystick();
    }

    private void Joystick()
    {
        basicMovement.ControlMovement(fixedJoystick.Horizontal, fixedJoystick.Vertical);
    }

}
