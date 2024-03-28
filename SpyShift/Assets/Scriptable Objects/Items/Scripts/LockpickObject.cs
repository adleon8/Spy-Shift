using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lockpick Object", menuName = "Inventory System/Items/Lockpick")]

public class LockpickObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Lockpick;
    }
}
