<<<<<<< HEAD
=======
using System.Collections;
using System.Collections.Generic;
>>>>>>> origin/main
using UnityEngine;

public class InteractableGeneral : Interactable
{
    [SerializeField]
    private GameObject currentObject;
<<<<<<< HEAD
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
=======
    // Start is called before the first frame update
    void Awake()
    {
        currentObject = gameObject; // 'this' elhagyható
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        
        Debug.Log($"Felvettük a követ: {promptMessage}");
        Destroy(gameObject);
        
>>>>>>> origin/main
    }
}
