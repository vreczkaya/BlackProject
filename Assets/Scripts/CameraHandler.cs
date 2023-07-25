using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform pivot;
    [SerializeField]
    private Transform character;
    [SerializeField]
    private Transform mainTransform;
    
    [SerializeField]
    private CharacterStatus characterStatus;
    [SerializeField]
    private CameraConfig cameraConfig;

    private bool isLeftPivot;

    private float mouseX;
    private float mouseY;
    private float smoothX;
    private float smoothY;
    private float lockAngle;
    private float titlAngle;

    private void Update()
    {
        HandlePosition();
        HandleRotation();

        Vector3 targetPosition = Vector3.Lerp(mainTransform.position, character.position, 0.5f);
        mainTransform.position = targetPosition;
    }

    private void HandlePosition()
    {
        float targetX = cameraConfig.NormalX;
        float targetY = cameraConfig.NormalY;
        float targetZ = cameraConfig.NormalZ;

        if (characterStatus.IsAiming)
        {
            targetX = cameraConfig.AimX;
            targetZ = cameraConfig.AimZ;
        }

        if (isLeftPivot)
        {
            targetX = -targetX;
        }

        Vector3 newPivotPosition = pivot.localPosition;

        newPivotPosition.x = targetX;
        newPivotPosition.y = targetY;

        Vector3 newCameraPosition = cameraTransform.localPosition;
        newCameraPosition.z = targetZ;

        float t = Time.deltaTime * cameraConfig.PivotSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newCameraPosition, t);
    }

    private void HandleRotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        
        smoothX = mouseX;
        smoothY = mouseY;

        lockAngle += smoothX * cameraConfig.XRotationSpeed * cameraConfig.Sensitivity;
        Quaternion targetRot = Quaternion.Euler(0, lockAngle, 0);
        mainTransform.rotation = targetRot;

        titlAngle -= smoothY * cameraConfig.YRotationSpeed * cameraConfig.Sensitivity;
        titlAngle = Mathf.Clamp(titlAngle, cameraConfig.MinAngle, cameraConfig.MaxAngle);
        pivot.localRotation = Quaternion.Euler(titlAngle, 0, 0);
    }
}