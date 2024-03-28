using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mine Object", menuName = "Inventory System/Items/Mine")]

public class MineObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Mine;
    }
}
