using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    public float chaseRange = 10f;
    public float moveSpeed = 5f;
    public float chaseSpeed;
    public float changeDirectionInterval = 3f;
    public LayerMask obstacleLayer;

    private Rigidbody enemyRigidbody;
    private bool isChasing = false;
    private Vector3 randomDirection;
    private float nextDirectionChangeTime = 0f;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        ChooseRandomDirection();
        player = PlayerController.Instance.transform;
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player color is different from enemy color
        bool differentColor = !player.GetComponent<Renderer>().material.color.Equals(GetComponent<Renderer>().material.color);

        // Check if player is crawling
        bool isPlayerCrawling = player.GetComponent<PlayerController>().IsCrawling;

        if (isChasing && differentColor && !isPlayerCrawling&&PlayerController.Instance.isSpawning==false)
        {
         //   isChasing = true;
            ChasePlayer();
        }
        else
        {
         //   isChasing = false;
            if (Time.time >= nextDirectionChangeTime)
            {
                ChooseRandomDirection();
                nextDirectionChangeTime = Time.time + changeDirectionInterval;
            }
            MoveRandomly();
        }
    }

    void ChasePlayer()
    {
        if (isChasing)
        {
           
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 movePosition = transform.position + direction * chaseSpeed * Time.deltaTime;
            enemyRigidbody.MovePosition(movePosition);

            // Rotate towards the player
            transform.LookAt(player);
        }
    }

    void MoveRandomly()
    {
        Vector3 movePosition = transform.position + randomDirection * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + randomDirection);
        enemyRigidbody.MovePosition(movePosition);
        if (Time.time >= nextDirectionChangeTime)
        {
            ChooseRandomDirection();
            nextDirectionChangeTime = Time.time + changeDirectionInterval;
        }
    }

    void ChooseRandomDirection()
    {
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            // Change direction upon colliding with a wall
            ChooseRandomDirection();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            isChasing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isChasing = false;
        }
    }
}