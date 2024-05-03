using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();

    private void OnEnable()
    {
        // Debugger to check if code is only running within unity editor.
#if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        database = Resources.Load<ScriptableObject>("Database");
        
#endif
    }
    
  

    public void AddItem(ItemObject _item, int _amount)
    {
        if (_item == null)
        {
            Debug.Log("Null item");
            return;
        }

        // Ensures the database is loaded
        if (database == null)
#if UNITY_EDITOR
            database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
            database = Resources.Load<ItemDatabaseObject>("Database");
#endif
        if (database == null)
        {
            Debug.Log("Fail database");
            return;
        }

        if (!database.GetId.ContainsKey(_item))
        {
            Debug.Log("Item not recognized by database: " + _item.name);
        }

        InventorySlot existingSlot = Container.Find(InventorySlot => InventorySlot.item == _item);
        if (existingSlot != null)
        {
            existingSlot.AddAmount(_amount);
        }
        else
        {
            Container.Add(new InventorySlot(database.GetId[_item], _item, _amount));
        }
    }

    public ItemObject GetItem(ItemType itemType)
    {
        foreach (var slot in Container)
        {
            if (slot.item.type == itemType)
            {
                return slot.item;
            }
        }
        return null;
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, savePath), saveData);
        Debug.Log("Saved Inventory: " + saveData);
    }

    public void Load()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, savePath);
        if (File.Exists(fullPath))
        {
            string saveData = File.ReadAllText(fullPath);
            JsonUtility.FromJsonOverwrite(saveData, this);
            Debug.Log("Loaded Inventory: " + saveData);
        }
        else
        {
            Debug.LogError("No save file found at " + fullPath);
        }
    }

    public void OnAfterDeserialize()
    {
        // Re-link items by ID from a loaded database, ensure the database is not called here.
        foreach (var slot in Container)
        {
            if (database != null && database.GetItem.ContainsKey(slot.ID))
                slot.item = database.GetItem[slot.ID];
            else
                Debug.LogError("Failed to link item ID: " + slot.ID);
        }
    }

    public void OnBeforeSerialize()
    {
    }
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public ItemObject item;
    public int amount;
    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}