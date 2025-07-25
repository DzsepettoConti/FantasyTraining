using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItem = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;

    [SerializeField] CanvasGroup inventoryCanvasGroup;
    private void Awake()
    {
        HideInventoryUI();
    }
    public void HideInventoryUI()
    {
        inventoryCanvasGroup.alpha = 0;
        inventoryCanvasGroup.blocksRaycasts = false;
        inventoryCanvasGroup.interactable = false;
    }

   public void ShowInventoryUI()
    {
        inventoryCanvasGroup.alpha = 1;
        inventoryCanvasGroup.blocksRaycasts = true;
        inventoryCanvasGroup.interactable = true;
    }


    private void Start()
    {
        changeSelectedSlot(0);
    }

    private void Update()
    {
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 8)
            {
                changeSelectedSlot(number - 1);
            }
        }
    }
    void changeSelectedSlot(int newValue)
    {
        if(selectedSlot >= 0) { 
        inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool Additem(Item item)
    {
        // Find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            if (slot == null)
            {
                Debug.LogError("Inventory slot at index " + i + " is null!");
                continue;
            }

            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                if (itemInSlot.item == item && itemInSlot.count < maxStackedItem && itemInSlot.item.stackable)
                {
                    itemInSlot.count++;
                    itemInSlot.RefreshCount();
                    return true;
                }
            }
        }
        // Find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++) 
        {
        InventorySlot slot = inventorySlots[i];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
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
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if(use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        return null;
    }
}
