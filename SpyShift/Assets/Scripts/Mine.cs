using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        Destroy(other.gameObject);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
