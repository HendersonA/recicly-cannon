using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private bool canMove = false, canRotate = false;

    private float vertical, horizontal, mouseX, mouseY;

    void Update()
    {
        ControlMovement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public void ControlMovement(float horizontalAxis, float verticalAxis)
    {
        if (canMove)
            Move(horizontalAxis, verticalAxis);
        if (canRotate)
            Rotate(horizontalAxis, verticalAxis);
    }

    private void Move(float horizontalAxis, float verticalAxis)
    {
        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            Vector3 playerMovement = new Vector3(horizontalAxis, 0f, verticalAxis) * moveSpeed * Time.deltaTime;
            transform.Translate(playerMovement, Space.Self);
        }
    }

    private void Rotate(float horizontalAxis, float verticalAxis)
    {
        float mouseInput = horizontalAxis * rotationSpeed;
        Vector3 lookhere = new Vector3(0, mouseInput, 0);
        transform.Rotate(lookhere);
    }
}
