using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")] // Creates it in the editor.
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items; // Array of all the items that exist within the game.
    public Dictionary<ItemObject, int> GetId = new Dictionary<ItemObject, int>();// Dictionary to import item and return ID of item.
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();// Dictionary to import ID and return item.

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<ItemObject, int>(); // Clears dictionary out to not duplicate anything.
        GetItem = new Dictionary<int, ItemObject>();

        for (int i = 0; i < Items.Length; i++) // Loops through all the items, automatically populates dictionary.
        {
            GetId.Add(Items[i], i); // ID is i.
            GetItem.Add(i, Items[i]);
        }


    }
    public void OnBeforeSerialize()
    {

    }
}
