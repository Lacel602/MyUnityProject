using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickUp;

    public void PickupItem(int id)
    {
        if (itemsToPickUp[id] == null)
        {
            Debug.Log("Item not found");
        }
        else
        {
            bool result = inventoryManager.AddItem(itemsToPickUp[id]);
            if (result)
            {
                Debug.Log("Item Added");
            } else
            {
                Debug.Log("Failed adding item!");
            }
        }
    }

    public void Remove(int id)
    {
        bool result = inventoryManager.RemoveItem(itemsToPickUp[id]);
        if (result)
        {
            Debug.Log("Item removed");
        }
        else
        {
            Debug.Log("Failed remove item!");
        }
    }
}
