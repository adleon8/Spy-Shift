using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tranquilizer : MonoBehaviour
{
    public float speed = 25f;

    private void Start()
    {
        Invoke("DestroySelf", 8);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyMovement>().enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyMovement>().enabled = false;
            Destroy(gameObject);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
