using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private const int SLOTS = 10;
    private List<IInventoryItems> mItems = new List<IInventoryItems>();
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public void AddItem(IInventoryItems item)
    {
        if (mItems.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;
                mItems.Add(item);
                item.OnPickup();

                if (ItemAdded != null)
                    ItemAdded(this, new InventoryEventArgs(item));
            }
        }
    }

    public void RemoveItem(IInventoryItems item)
    {
        if(mItems.Contains(item))
        {
            mItems.Remove(item);
            item.OnDrop();

            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider != null)
                collider.enabled = true;

            if (ItemRemoved != null)
                ItemRemoved(this, new InventoryEventArgs(item));
        }
    }
}
