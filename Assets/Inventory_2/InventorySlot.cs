using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class InventorySlot
{
    private Stack<IInventoryItems> mItemStack = new Stack<IInventoryItems>();

    private int mID = 0;

    public InventorySlot(int id)
    {
        mID = id;
    }

    public int Id
    {
        get { return mID; }
    }

    public void AddItem(IInventoryItems item)
    {
        item.Slot = this;
        mItemStack.Push(item);
    }

    public IInventoryItems FirstItem
    {
        get
        {
            if (isEmpty)
                return null;

            return mItemStack.Peek();
        }
    }

    public bool IsStackable(IInventoryItems item)
    {
        if (isEmpty)
            return false;

        IInventoryItems first = mItemStack.Peek();

        if (first.Name == item.Name)
            return true;

        return false;
    }

    public bool isEmpty
    {
        get { return Count == 0; }
    }

    public int Count
    {
        get { return mItemStack.Count; }
    }

    public bool Remove(IInventoryItems item)
    {
        if (isEmpty)
            return false;

        IInventoryItems first = mItemStack.Peek();
        if (first.Name == item.Name)
        {
            mItemStack.Pop();
            return true;
        }

        return false;
    }
}
