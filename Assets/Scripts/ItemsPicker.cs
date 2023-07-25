using System;
using UnityEngine;

public class ItemsPicker : SearchQuest
{
    public Action<InventoryItem> OnItemPicked;

    [SerializeField]
    private InventoryItem property;

    private void Start()
    {
        property.IsPicked = isFound;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.tag == playersTag && !property.IsPicked)
        {
            property.IsPicked = isFound;
            OnItemPicked?.Invoke(property);
        }
    }
}
