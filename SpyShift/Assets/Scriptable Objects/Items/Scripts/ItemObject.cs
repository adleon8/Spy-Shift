using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Projectile,
    Coin, 
    Key, 
    Lockpick,
    Mine, 
    Files, 
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public GameObject worldPrefab;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
}
