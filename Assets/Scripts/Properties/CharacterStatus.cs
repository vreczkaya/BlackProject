using UnityEngine;

[CreateAssetMenu(menuName = "Character/Status")]
public class CharacterStatus : ScriptableObject
{
    public bool IsAiming;
    public bool IsSprinting;
    public bool IsGrounded;
    public bool IsDead;
    public bool HasWeapon;
    public WeaponType WeaponType;
}
