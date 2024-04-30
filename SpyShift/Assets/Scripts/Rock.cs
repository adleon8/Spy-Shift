using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private float speed=2;

    private bool isOnPlayer;

    private void Update()
    {
        if (isOnPlayer)
        {
            transform.position = GameObject.Find("Player").transform.position + Vector3.up * 2.5f;
            transform.rotation = GameObject.Find("Player").transform.rotation;
        }
    }

    public void SetOnPlayer()
    {
        isOnPlayer = !isOnPlayer;
        transform.GetComponent<Rigidbody>().useGravity = !isOnPlayer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
    //public void Fly(float shootingForce)
    //{
    //    StartCoroutine(FlyCoroutine(shootingForce));
    //}

    //private IEnumerator FlyCoroutine(float shootingForce)
    //{
    //    float flyingTime = shootingForce / speed;
    //    float currentTime = 0;
    //    while (currentTime<flyingTime)
    //    {
    //        transform.Translate(new Vector3(0,speed*0.5f,speed)*0.2f);
    //        currentTime += 0.02f;
    //        yield return new WaitForSeconds(0.02f);
    //    }
    //    while (!Physics.Raycast(transform.position,transform.TransformDirection(Vector3.down),1.5f))
    //    {
    //        transform.Translate(new Vector3(0, -speed*0.5f, speed) * 0.2f);
    //        yield return new WaitForSeconds(0.02f);
    //    }
    //}
}
