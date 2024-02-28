using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefabs;
    public Transform launchPoint;
    private NewPlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<NewPlayerController>();
    }
    public void FireArrow()
    {
        //Remove arrow from inventory
        playerController.inventoryManager.RemoveItem(playerController.arrow);

        //Spawn arrow to map
        GameObject spawnedArrow = Instantiate(arrowPrefabs, launchPoint.position, arrowPrefabs.transform.rotation);
        spawnedArrow.transform.localScale = new Vector3 (spawnedArrow.transform.localScale.x * transform.localScale.x > 0 ? 1f : -1f, spawnedArrow.transform.localScale.y, spawnedArrow.transform.localScale.z);
        Arrow arrow = spawnedArrow.GetComponent<Arrow>();

        //Set charge for arrow
        arrow.mutiplier = playerController.charged;
        playerController.bowHoldingTime = 0;
        //Debug.Log("Mutiplier = " + arrow.mutiplier);      
    }

}
