using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveDir;
    private Rigidbody rigid;
    private int speed=5;
    public float minSpinDistance = 2;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity =transform.TransformDirection(new Vector3(moveDir.x,rigid.velocity.y,moveDir.y));
    }

    public void Move(InputAction.CallbackContext input )
    {
        //if (input.performed)
        //{
        //    Vector2 v= input.ReadValue<Vector2>();
        //    rigid.velocity = new Vector3(v.x, 0, v.y);
        //}
        moveDir = input.ReadValue<Vector2>()*speed;
    }

    public void Spin()
    {
        //GameObject e = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().GetNearestEnemy();
        //if (Vector3.Distance(transform.position,e.transform.position)<=minSpinDistance)
        //{
        //    GetComponent<MeshRenderer>().material.color = e.GetComponent<MeshRenderer>().material.color;
        //}
        RaycastHit hit;
        if( Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit,minSpinDistance))
        {
            if (hit.transform.gameObject.tag=="Enemy")
            {
                GetComponent<MeshRenderer>().material.color = hit.transform.GetComponent<MeshRenderer>().material.color;
            }
        }
    }
}
