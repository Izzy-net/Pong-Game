using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class OpponentBehaviour : MonoBehaviour
{
    private Rigidbody2D oppRigidbody;
    private BoxCollider2D oppCollider;
    private float oppHeight;
    private float direction;
    GameObject ball;
    [SerializeField] float moveSpeedDef = 2f;
    private float moveSpeed;

    void Start()
    {
        ball = FindFirstObjectByType<BallBehaviour>().gameObject;
        oppRigidbody = GetComponent<Rigidbody2D>();
        oppCollider = GetComponent<BoxCollider2D>();
        oppHeight = oppCollider.bounds.max.y - oppCollider.bounds.min.y;
    }

    void Update()
    {
        if (ball.transform.position.x < 0)
        {
            oppRigidbody.linearVelocityY = 0;
        }
        else
        {
            FollowBall();
        }
    }

    private void FollowBall()
    {
        if (ball.transform.position.y > transform.position.y + (oppHeight/2)*0.5)
        {
            direction = 1f;
        }
        else if (ball.transform.position.y < transform.position.y - (oppHeight/2)*0.5)
        {
            direction = -1f;
        }

        if (ball.transform.position.y - transform.position.y > (oppHeight/2)*0.5)
        {
            moveSpeed = moveSpeedDef;
        }
        else 
        {
            float angle = Mathf.Atan2(ball.transform.position.y - transform.position.y, ball.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            float slowFactor = (180 - Mathf.Abs(angle)) * 0.1f;
            moveSpeed = moveSpeedDef * slowFactor;
        }
        oppRigidbody.linearVelocityY = direction * moveSpeed;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(transform.position, oppHeight/2);
    }
}
