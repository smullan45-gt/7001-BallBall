using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float turnSpeed;

    public InputAction moveAction;
    public InputAction batAction;

    public bool rotateLock = true;

    private Rigidbody rigidBody;

    private Vector2 moveInput;

    private Vector3 currentPosition;

    private Quaternion currentRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentPlacement();
        CheckRotateLock();
    }

    void GetCurrentPlacement()
    {
        currentPosition = transform.position;
        currentRotation = transform.rotation;
    }

    void CheckRotateLock()
    {
        if(rotateLock)
        {
            LockRotation();
            CheckMoveInput();
        }
    }

    void LockRotation()
    {
        float rotationY = rigidBody.rotation.y;
        rigidBody.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    void CheckMoveInput()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        Vector3 finalMove = new Vector3(moveInput.x, 0.0f, moveInput.y).normalized; //Normalised prevents unnatural acceleration on the diagonal
        transform.Translate(finalMove * speed * Time.deltaTime, Space.World);

        Quaternion targetRotation = Quaternion.LookRotation(finalMove, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public void SetRotateLock(bool isLocked)
    {
        rotateLock = isLocked;
    }
}
