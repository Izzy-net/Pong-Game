using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] private Vector2 currentTrajectory;
    private Rigidbody2D ballRigidbody;
    private GameObject player;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float knockForce = 40f;  //force applied by the direction of movement of the player/opponent
    
    private void Awake() 
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerController>().gameObject;
    }

    void Start()
    {
        LaunchBall();
    }

    void Update()
    {
        //Move();
    }

    private void Move()
    {
        ballRigidbody.linearVelocity = currentTrajectory * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        currentTrajectory = Vector2.Reflect(currentTrajectory, other.gameObject.transform.right);
        ballRigidbody.AddForceY(other.otherRigidbody.linearVelocityY * knockForce);
        transform.right = currentTrajectory;
        Move();
    }

    private void LaunchBall()
    {
        //launch ball towards player at random angle
        RandomAngle();
        currentTrajectory = transform.right;
        Move();
    }

    private void RandomAngle()
    {
        float yRange = Camera.main.orthographicSize;
        var direction = new Vector2 (player.transform.position.x, UnityEngine.Random.Range(yRange, -yRange));
        transform.right = direction;
    }
}
