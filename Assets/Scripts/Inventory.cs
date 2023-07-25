using System;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<InventoryItem> inventoryItems;

    [SerializeField]
    private ItemsPicker itemsPicker;

    public Action<InventoryItem> OnItemAdded;

    public List<InventoryItem> GetInventoryItems() => inventoryItems;

    private void Start()
    {
        itemsPicker.OnItemPicked += AddInventoryItem;
    }

    public void AddInventoryItem(InventoryItem item)
    {
        inventoryItems.Add(item);
        OnItemAdded?.Invoke(item);
    }

    private void OnApplicationQuit()
    {
        itemsPicker.OnItemPicked -= AddInventoryItem;
    }
}
