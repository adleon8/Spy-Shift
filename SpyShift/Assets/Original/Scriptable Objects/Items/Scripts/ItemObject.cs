using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemObject : ScriptableObject
{
    public GameObject inventoryPrefab;
    public GameObject worldPrefab;
    public ItemType type;
    [TextArea(15,20)]
    public string description;

    // Method to define what will happen when item is used.
    public virtual void Use(GameObject player)
    {
        // This method is to be overriden in subclasses that need specific actions to happen.
        Debug.Log("Item used: " + this.name);
    }
}
