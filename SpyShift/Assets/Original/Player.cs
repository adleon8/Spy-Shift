using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject inventoryScreen; // Reference to the inventory UI screen

    public static Player instance; // Singleton pattern to ensure only one instance of Player exists

    public float moveSpeed = 5.0f; // Speed at which the player moves

    void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the player object consistent across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Ensures only one instance of this GameObject exists
            return;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // Check for collision with items and add them to the inventory
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject); // Destroy the item after picking it up
        }
    }

    private void Update()
    {
        /* NOT NEEDED
        // Handle player movement input
        float moveHorizontal = Input.GetAxis("Horizontal"); // Gets horizontal movement (A and D keys)
        float moveVertical = Input.GetAxis("Vertical"); // Gets vertical movement (W and S keys)

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World); // Move the player



        */

        // Toggle inventory screen visibility
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryScreen.SetActive(!inventoryScreen.activeSelf);
        }

        // Save the inventory when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
        }

        // Load the inventory when the enter key on the keypad is pressed
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
        }
    }

    private void OnApplicationQuit()
    {
        // Clear the inventory when the application quits
        inventory.Container.Clear();
    }

}
