using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [HideInInspector]
    public float Vertical, Horizontal, MoveAmount;

    [SerializeField] 
    private Transform cameraTransform;
    [SerializeField] 
    private CharacterStatus characterStatus;
    [SerializeField] 
    private float rotationSpeed;

    private Vector3 rotationDirection, moveDirection;

    public void UpdateMovement()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");

        MoveAmount = Mathf.Clamp01(Mathf.Abs(Vertical) + Mathf.Abs(Horizontal));

        Vector3 moveDir = cameraTransform.forward * Vertical;
        moveDir += cameraTransform.right * Horizontal;
        moveDir.Normalize();
        moveDirection = moveDir;

        rotationDirection = cameraTransform.forward;

        NormalizeRotation();

        characterStatus.IsGrounded = IsGrounded();
    }

    private void NormalizeRotation()
    {
        if (!characterStatus.IsAiming)
        {
            rotationDirection = moveDirection;
        }

        Vector3 targetDirection = rotationDirection;
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion lookDirection = Quaternion.LookRotation(targetDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, lookDirection, rotationSpeed);

        transform.rotation = targetRotation;
    }

    private bool IsGrounded()
    {
        Vector3 origin = transform.position;
        origin.y += 0.6f;

        Vector3 direction = Vector3.down;
        float distance = 0.7f;

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, distance))
        {
            Vector3 tp = hit.point;
            transform.position = tp;
            return true;
        }
        return false;
    }
}
