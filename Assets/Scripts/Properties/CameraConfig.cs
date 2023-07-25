using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Config")]
public class CameraConfig : ScriptableObject
{
    public float PivotSpeed;
    public float YRotationSpeed;
    public float XRotationSpeed;

    [Range(-180f, 180f)]
    public float MinAngle;
    [Range(-180f, 180f)]
    public float MaxAngle;

    public float NormalX;
    public float NormalY;
    public float NormalZ;

    public float AimX;
    public float AimZ;

    [Range(0f, 1f)]
    public float Sensitivity;
}
