using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Coin Object", menuName = "Inventory System/Items/Coin")]

public class CoinObject : ItemObject
{
    public int addCoinValue;

    public void Awake()
    {
        type = ItemType.Coin;
    }
}
