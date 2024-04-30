using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required for handling hover

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public int x_start;
    public int y_start;
    public int x_space_between_item;
    public int number_of_column;
    public int y_space_between_item;
    public GameObject descriptionPanel;
    public TextMeshProUGUI descriptionText;
    public GameObject inventoryPanel;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    private bool inventoryVisible = false; 

    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel.SetActive(false);
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        UpdateDisplay();
    }

    void ToggleInventory()
    {
        inventoryVisible = !inventoryVisible;
        inventoryPanel.SetActive(inventoryVisible);
    }

    public void UpdateDisplay()
    {
        // Temporary list to track items that need to be removed from the display
        List<InventorySlot> itemsToRemove = new List<InventorySlot>();

        // Check existing displayed items and mark for removal if they are no longer in the inventory
        foreach (var displayed in itemsDisplayed)
        {
            if (!inventory.Container.Contains(displayed.Key))
            {
                // If the item is no longer in the inventory, schedule it for removal
                itemsToRemove.Add(displayed.Key);
            }
            else
            {
                // Update the count on the existing UI elements
                displayed.Value.GetComponentInChildren<TextMeshProUGUI>().text = displayed.Key.amount.ToString("n0");
                // Ensure the item's position is updated in case other items have been removed
                int newIndex = inventory.Container.IndexOf(displayed.Key);
                displayed.Value.GetComponent<RectTransform>().localPosition = GetPosition(newIndex);
            }
        }

        // Remove the outdated UI elements
        foreach (var item in itemsToRemove)
        {
            Destroy(itemsDisplayed[item]);
            itemsDisplayed.Remove(item);
        }

        // Add new items to the display if they are not already displayed
        foreach (var item in inventory.Container)
        {
            if (!itemsDisplayed.ContainsKey(item))
            {
                var obj = CreateItemButton(item, inventory.Container.IndexOf(item));
                itemsDisplayed.Add(item, obj);
            }
        }
    }

    GameObject CreateItemButton(InventorySlot slot, int index)
    {
        // Instantiates item's prefab as UI element within the inventory display.
        var obj = Instantiate(slot.item.inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(index);

        // Create a new GameObject for the text component to avoid conflict with the Image component.
        GameObject textObj = new GameObject("ItemText");
        textObj.transform.SetParent(obj.transform); // Set the text object as a child of the main object.

        // Add and set up TextMeshProUGUI component.
        var textMesh = textObj.AddComponent<TextMeshProUGUI>();
        textMesh.text = slot.amount.ToString("n0");
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.fontSize = 24; // Set font size.

        // Adjust the RectTransform to fill the parent object.
        RectTransform rectTransform = textObj.GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.sizeDelta = new Vector2(100, 50); // Adjust size as needed to fit within the parent.
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMin = new Vector2(0, 0); // Left bottom
        rectTransform.offsetMax = new Vector2(0, 0); // Right top

        // Adds a Button component if it doesn't exist
        var button = obj.GetComponent<Button>();
        if (button == null)
        {
            button = obj.AddComponent<Button>();
        }

        // Configures the button's click event
        button.onClick.AddListener(() => DropItem(slot));

        // Adds hover effects with EventTrigger
        var eventTrigger = obj.AddComponent<EventTrigger>();
        var pointerEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        pointerEnter.callback.AddListener((data) => { ShowDescription(slot.item); });
        eventTrigger.triggers.Add(pointerEnter);

        var pointerExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        pointerExit.callback.AddListener((data) => { HideDescription(); });
        eventTrigger.triggers.Add(pointerExit);

        return obj;
    }

    public void ShowDescription(ItemObject item)
    {
        // Show description UI element, perhaps a tooltip or a dedicated UI panel.
        descriptionPanel.SetActive(true);
        descriptionText.text = item.description;
    }

    void HideDescription()
    {
        // Hide the description UI element.
        descriptionPanel.SetActive(false);
    }

    void DropItem(InventorySlot slot)
    {
        // This method manages item dropping logic.
        Vector3 dropPosition = PlayerController.Instance.transform.position + PlayerController.Instance.transform.forward * 1.5f;

        Instantiate(slot.item.worldPrefab, dropPosition, Quaternion.identity);

        slot.amount -= 1;

        // If the amount is zero, remove item slot from inventory and display.
        if (slot.amount > 0)
        {
            UpdateItemDisplay(slot);
        }
        else
        {
            inventory.Container.Remove(slot);
            if (itemsDisplayed.ContainsKey(slot))
            {
                Destroy(itemsDisplayed[slot]);
                itemsDisplayed.Remove(slot);
            }

            UpdateDisplay();
        }
    }

    void UpdateItemDisplay(InventorySlot slot)
    {
        // Updates display of the item amount if there are some left.
        if (itemsDisplayed.TryGetValue(slot, out var itemDisplay))
        {
            TextMeshProUGUI textMesh = itemDisplay.GetComponentInChildren<TextMeshProUGUI>();
            if (textMesh != null)
            {
                textMesh.text = slot.amount.ToString("n0");
            }
            else
            {
                Debug.LogError("UpdateItemDisplay: TextMeshProUGUI component is missing on item display");
            }
        }
        else
        {
            Debug.LogError("UpdateItemDisplay: Display object for slot not found in itemsDisplayed dictionary");
        }
    }

    public void CreateDisplay()
    {
        foreach (GameObject displayedItem in itemsDisplayed.Values)
        {
            Destroy(displayedItem);
        }
        itemsDisplayed.Clear();

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = CreateItemButton(inventory.Container[i], i);
            itemsDisplayed.Add(inventory.Container[i], obj);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(x_start + (x_space_between_item * (i % number_of_column)), y_start + (-y_space_between_item * (i / number_of_column)), 0f);
    }
}