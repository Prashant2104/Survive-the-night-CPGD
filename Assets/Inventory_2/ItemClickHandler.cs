using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{
    public InventoryManager _Inventory;
    
    public void OnItemClicked()
    {
        ItemDragHandler dragHandler = gameObject.transform.Find("Image").GetComponent<ItemDragHandler>();
        IInventoryItems item = dragHandler.Item;
        
        Debug.Log(item.Name);

        _Inventory.UseItem(item);
        
        item.OnUse();
    }
}
