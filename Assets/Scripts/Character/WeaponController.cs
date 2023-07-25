using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Weapon[] weapons;

    [SerializeField]
    private CharacterStatus characterStatus;

    private KeyboardInput keyboardInput;

    private Weapon activeWeapon;
    public Weapon GetActiveWeapon() => activeWeapon;

    private void Awake()
    {
        keyboardInput = GetComponent<KeyboardInput>();
        keyboardInput.OnSwitchedWeapon.AddListener(SetActiveWeapon);
        SetActiveWeapon();
    }

    private Weapon SelectWeapon()
    {
        if (characterStatus.WeaponType == WeaponType.MachineGun)
        {
            TurnOffAllWeapon();
            TurnOnWeapon(weapons[0]);
            return weapons[0];
        }
        else if (characterStatus.WeaponType == WeaponType.Rocket &&
            weapons[1].weaponProperty.IsPicked)
        {
            TurnOffAllWeapon();
            TurnOnWeapon(weapons[1]);
            return weapons[1];
        }

        TurnOffAllWeapon();
        characterStatus.HasWeapon = false;
        return null;    
    }

    private void SetActiveWeapon()
    {
        activeWeapon = SelectWeapon();
    }

    private void Update()
    {
        SetActiveWeapon();
    }

    public WeaponProperty GetWeaponProperty()
    {
        SetActiveWeapon();
        return activeWeapon.weaponProperty;
    }

    private void TurnOffAllWeapon()
    {
        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
    }

    private void TurnOnWeapon(Weapon weapon)
    {
        weapon.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        keyboardInput.OnSwitchedWeapon.RemoveListener(SetActiveWeapon);
    }
}
