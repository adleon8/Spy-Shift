using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null && playerController.hasKey)
        {
            // Open the door
            animator.SetBool("isOpen", true);
            // Use the key
            playerController.hasKey = false;
        }
    }
}
