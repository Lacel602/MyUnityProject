using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public int itemId;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = GameObject.FindWithTag("InventoryManage").GetComponent<InventoryManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Increase item in inventory
            if (itemId < inventoryManager.itemsToPickUp.Length)
            {
                inventoryManager.AddItem(inventoryManager.itemsToPickUp[itemId]);
            }
            //Destroy obj

            Destroy(transform.parent.gameObject);
        }
    }
}
