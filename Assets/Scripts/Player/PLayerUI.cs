using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private InventoryManager inventoryManager;

    private bool isInventoryOpen = false;

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if (isInventoryOpen)
        {
            inventoryManager.ShowInventoryUI();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            inventoryManager.HideInventoryUI();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
