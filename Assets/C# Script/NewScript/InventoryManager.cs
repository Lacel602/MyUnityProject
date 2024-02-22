using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItem = 5;
    public Item[] itemsToPickUp;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public bool RemoveItem(Item item)
    {
        for (int i = inventorySlots.Length - 1; i >= 0; i--)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            //if slot is not occupied
            if (itemInSlot != null && itemInSlot.item == item)
            {
                if (itemInSlot.count > 1)
                {
                    itemInSlot.count--;
                    itemInSlot.RefreshCount();
                }
                else
                {
                    Destroy(itemInSlot.gameObject);
                }
                return true;
            }
        }

        return false;
    }
    public bool AddItem(Item item)
    {
        //Check duplicate item
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            //if slot is not occupied
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count <= maxStackedItem && itemInSlot.item.stackable)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        //Find empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            //if slot is not occupied
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject spawnedItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = spawnedItem.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
}
