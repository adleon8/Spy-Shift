using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mine Object", menuName = "Inventory System/Items/Mine")]

public class MineObject : ItemObject
{
    public GameObject minePrefab;

    public void Awake()
    {
        type = ItemType.Mine;
    }

    public override void Use(GameObject player)
    {
        if (minePrefab != null)
        {
            // Instantiates mine prefab at the player's position
            Instantiate(minePrefab, player.transform.position + Vector3.down * 0.5f, Quaternion.identity);
        }
        else
        {
            Debug.Log("Mine prefab unassigned");
        }
    }
}
