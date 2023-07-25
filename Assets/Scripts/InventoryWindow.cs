using UnityEngine;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private KeyboardInput keyboardInput;

    private bool isActive = true;

    private void Start()
    {
        ShowWindow();
        SetInventoryItems();

        keyboardInput.OnQPressed.AddListener(ShowWindow);
        inventory.OnItemAdded += AddItem;
    }

    private void ShowWindow()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
    }

    private void SetInventoryItems()
    {
        foreach (var item in inventory.GetInventoryItems())
        {
            AddItem(item);
        }
    }

    private void AddItem(InventoryItem item)
    {
        var newItem = Instantiate(item.ItemTile);
        newItem.transform.SetParent(inventory.gameObject.transform);

        var textName = Instantiate(item.TextName);
        textName.text = item.ItemName;
        textName.transform.SetParent(newItem.transform);

        var icon = Instantiate(item.IconImage);
        icon.sprite = item.IconTexture;
        icon.transform.SetParent(newItem.transform);
    }

    private void OnApplicationQuit()
    {
        keyboardInput.OnQPressed.RemoveListener(ShowWindow);
        inventory.OnItemAdded -= AddItem;
    }
}
