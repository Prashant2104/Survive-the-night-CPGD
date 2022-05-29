using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public interface IInventoryItems
{
    string Name { get; }
    Sprite Image { get; }
    void OnPickup();
    void OnDrop();
    void OnUse();

    InventorySlot Slot { get; set; }
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IInventoryItems item)
    {
        Item = item;
    }

    public IInventoryItems Item;
}
