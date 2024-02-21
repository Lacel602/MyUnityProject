using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private GameObject dragItem;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        { 
            dragItem = eventData.pointerDrag;
            InventoryItem inventoryItem = dragItem.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }

}
