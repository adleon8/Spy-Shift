using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TranquilizerGun Object", menuName = "Inventory System/Items/TranquilizerGun")]

public class TranquilizerGun : ItemObject
{
    // Prefab for the dart.
    public GameObject dartPrefab;
    public float dartSpeed = 25f;

    public void Awake()
    {
        type = ItemType.TranquilizerGun;
    }

    public override void Use(GameObject player)
    {
        Debug.Log("Tranquilizer gun used!");
        if (dartPrefab != null )
        {
            // Creates the dart at player's position
            GameObject dart = Instantiate (dartPrefab, player.transform.position + player.transform.forward, player.transform.rotation);
            Rigidbody dartRigidbody = dart.GetComponent<Rigidbody>();
            if (dartRigidbody != null )
            {
                dartRigidbody.velocity = player.transform.forward * dartSpeed;
            }
            else
            {
                // Ensures dart has a Rigidbody.
                dart.AddComponent<Rigidbody>().velocity = player.transform.forward * dartSpeed;
            }
            Debug.Log("Tranquilizer gun used just now.");
        }
        else
        {
            Debug.LogError("Dart prefab is not assigned.");
        }
    }
}
