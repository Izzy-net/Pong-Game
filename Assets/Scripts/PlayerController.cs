using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 playerInput;
    private Rigidbody2D myRigidbody;
    [SerializeField] float moveSpeed = 10f;

    private void Awake() 
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Move();
    }

    private void OnMove(InputValue inputValue)
    {
        var input = inputValue.Get<Vector2>();
        playerInput = new Vector2 (0, input.y* moveSpeed);
    }

    private void Move()
    {
        myRigidbody.linearVelocityY = playerInput.y;
    }
}
