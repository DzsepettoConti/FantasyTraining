
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InteractableGeneral : Interactable
{
    [SerializeField]
    private GameObject currentObject;

    public Item inventoryItem;
    public InventoryManager inventoryManager;

    void Awake()
    {
        currentObject = gameObject;

        GameObject managerGO = GameObject.FindWithTag("InventoryManager");
        if (managerGO != null)
        {
            inventoryManager = managerGO.GetComponent<InventoryManager>();
        }
    }

    protected override void Interact()
    {
        Debug.Log($"Felvettük a {currentObject}");
        bool resutl = inventoryManager.Additem(inventoryItem);
        if (resutl == true)
        {
            Debug.Log("Item added");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Item not added");
        }
    }
}
