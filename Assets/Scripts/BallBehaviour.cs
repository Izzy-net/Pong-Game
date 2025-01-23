using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Scripting.APIUpdating;

public class BallBehaviour : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Vector2 currentTrajectory;
    private Rigidbody2D ballRigidbody;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float knockForce = 40f;  //force applied by the direction of movement of the player/opponent
    private GameObject player;
    [Header("Visual Mode")]
    [SerializeField] public static bool predictiveMode = false;
    private SpriteRenderer ballSprite;
    [Header("Light")]
    [SerializeField] private LightController lightController;
    [SerializeField] private Light2D ballLight;
    [SerializeField] private float flashDuration = 0.5f;
    [SerializeField] private float flashIntensity = 3f;
    
    private void Awake() 
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerController>().gameObject;
        ballSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        LaunchBall();
    }

    private void Move()
    {
        ballRigidbody.linearVelocity = currentTrajectory * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag != "Bounds")
        {
            StartCoroutine(lightController.LightFlash(ballLight, flashDuration, flashIntensity));
        }
        currentTrajectory = Vector2.Reflect(currentTrajectory, other.gameObject.transform.right);
        ballRigidbody.AddForceY(other.otherRigidbody.linearVelocityY * knockForce);
        transform.right = currentTrajectory;
        Move();
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Centre")
        {
            if (transform.position.x < 0)
            {
                ballSprite.enabled = true;
                ballLight.enabled = true;
            }
            else if (transform.position.x > 0)
            {
                ballSprite.enabled = false;
                ballLight.enabled = false;
            }
            
            if (predictiveMode)
            {
                ballSprite.enabled = !ballSprite.enabled;
                ballLight.enabled = !ballLight.enabled;
            }
        }
        else if (other.gameObject.TryGetComponent<WinLose>(out var winLose)) //== "Win" || other.gameObject.tag == "Lose")
        {
            StartCoroutine(StopBall());
            winLose.HandleWinLose(other.gameObject.tag);
        }
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
        var aimTowards = player.transform.position.x;

        if (predictiveMode)
        {
            aimTowards = -aimTowards;
        }
        var direction = new Vector2 (aimTowards, UnityEngine.Random.Range(yRange, -yRange));
        transform.right = direction;
    }

    private IEnumerator StopBall()
    {
        yield return new WaitForSeconds(2);
        ballRigidbody.linearVelocity = Vector2.zero;
    }
}
