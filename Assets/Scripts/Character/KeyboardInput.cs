using UnityEngine;
using UnityEngine.Events;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField]
    private CharacterStatus characterStatus;
    [HideInInspector]
    public UnityEvent OnSpaceButtonPressed;
    [HideInInspector]
    public UnityEvent OnSwitchedWeapon;
    [HideInInspector]
    public UnityEvent OnQPressed;

    private WeaponController characterInventory;

    private void Start()
    {
        characterInventory = GetComponent<WeaponController>();
    }

    public void UpdateInput()
    {
        InputAiming();
        JumpInput();
        SwitchWeapon();
        ShowInventory();
    }

    private void ShowInventory()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnQPressed?.Invoke();
        }
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && characterStatus.IsGrounded)
        {
            OnSpaceButtonPressed?.Invoke();
        }
    }

    private void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && !characterStatus.IsAiming)
        {
            SelectWeapon(WeaponType.None);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(WeaponType.MachineGun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(WeaponType.Rocket);
        }
        OnSwitchedWeapon?.Invoke();
    }

    private void SelectWeapon(WeaponType weaponType)
    {
        characterStatus.WeaponType = weaponType;
        characterStatus.HasWeapon = weaponType > 0;
    }

    private void InputAiming()
    {
        if (Input.GetKeyDown(KeyCode.E) && characterStatus.HasWeapon)
        {
            characterStatus.IsAiming = !characterStatus.IsAiming;
            
        }
        if (Input.GetMouseButtonDown(0) && characterStatus.IsAiming)
        {
            characterInventory.GetActiveWeapon().Shoot();
        }
    }
}
