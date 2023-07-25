using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Properties")]
public class WeaponProperty : InventoryItem
{
    public Vector3 rightHandPos;
    public Vector3 rightHandRot;

    public GameObject weaponPrefab;
}
