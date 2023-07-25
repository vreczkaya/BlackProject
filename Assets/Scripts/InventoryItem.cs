using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string ItemName;
    public bool IsPicked;

    public Sprite IconTexture;
    public Image IconImage;
    public Text TextName;

    public GameObject ItemTile;
}
