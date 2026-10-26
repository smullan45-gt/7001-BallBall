using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float turnSpeed;

    public InputAction moveAction;
    public InputAction batAction;

    private Rigidbody rigidBody;

    private Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        Vector3 finalMove = new Vector3(moveInput.x, 0.0f, moveInput.y);
        rigidBody.AddForce(finalMove * speed * Time.fixedDeltaTime);
    }
}
