using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public int x_start;
    public int y_start;
    public int x_space_between_item;
    public int number_of_column;
    public int y_space_between_item;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }

    // Drops off item from inventory into the game world.
    void DropItem(InventorySlot slot)
    {
        // Checks if player has any of the item left to drop.
        if (slot.amount <= 0)
            return;

        // Instantiates item in the game world at the player's position.
        var droppedItem = Instantiate(slot.item.prefab, Player.instance.transform.position, Quaternion.identity);
        // Maybe add rigid body here?

        // Decrease the amount of the item in the inventory
        slot.amount -= 1;

        // If the amount is zero, remove item slot from inventory and display.
        if (slot.amount <= 0)
        {
            inventory.Container.Remove(slot);
            Destroy(itemsDisplayed[slot]);
            itemsDisplayed.Remove(slot);
        }
        else
        {
            // Updates display of the item amount if there are some left.
           // itemsDisplayed[slot]GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
        }

        UpdateDisplay();
    }



    GameObject CreateItemButton(InventorySlot slot, int index)
    {
        // Instantiates item's prefab as UI element within the inventory display.
        var obj = Instantiate(slot.item.prefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(index);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");

        // Adds a Button component if it doesn't exist
        var button = obj.GetComponent<Button>();
        if (button == null)
        {
            button = obj.AddComponent();
        }

        // Configures the button's click event
        button.onClick.RemoveAllListeners(); // to avoid duplicates.
        button.onClick.AddListener(() => DropItem(slot));




    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            itemsDisplayed.Add(inventory.Container[i], obj);
        }


    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(x_start + (x_space_between_item * (i % number_of_column)), y_start + (-y_space_between_item * (i / number_of_column)), 0f);
    }
}
