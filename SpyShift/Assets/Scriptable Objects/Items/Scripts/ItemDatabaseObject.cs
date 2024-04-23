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
        GetId.Clear();
        GetItem.Clear();
        for (int i = 0; i < Items.Length; i++)
        {
            if (!GetId.ContainsKey(Items[i]))
            {
                GetId.Add(Items[i], i);
                GetItem.Add(i, Items[i]);
            }
            else
            {
                Debug.LogError("Duplicate item detected in ItemDatabase: " + Items[i].name);
            }
        }

        

    }
    public void OnBeforeSerialize()
    {

    }
}
