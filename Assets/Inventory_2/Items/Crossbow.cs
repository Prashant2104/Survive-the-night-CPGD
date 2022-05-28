using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour, IInventoryItems
{
    public string Name
    {
        get{ return "Crossbow"; }
    }
    public Sprite _Image = null;
    public Sprite Image
    { 
        get{ return _Image; }
    }
    public void OnPickup()
    {
        // TODO: Add logic what happens when axe is picked up by player
        gameObject.SetActive(false);
    }
    public void OnDrop()
    {
        //TODO: Move a logic like this to a base helper class
        RaycastHit hit;
        Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }
}
