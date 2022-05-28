using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle_2 : MonoBehaviour, IInventoryItems
{
    public string Name
    {
        get { return "Rifle_2"; }
    }
    public Sprite _Image = null;
    public Sprite Image
    {
        get { return _Image; }
    }
    public void OnPickup()
    {
        // TODO: Add logic what happens when axe is picked up by player
        gameObject.SetActive(false);
    }
    public void OnDrop()
    {
        RaycastHit hit;
        Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }
}