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
        CheckRotateLock();
    }

    void CheckRotateLock() //Check if the player's rotation is locked (this will mean they haven't been hit so far)
    {
        if(rotateLock) //if they haven't been hit, lock XZ rotation and allow player input
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
        transform.Translate(finalMove * speed * Time.deltaTime, Space.World); //Space world uses universal axis for movement, so character rotation doesn't interfere with input

        Quaternion targetRotation = Quaternion.LookRotation(finalMove, Vector3.up); //Sets the direction to look, based on player input, rotating around Y axis
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime); //Smooth rotation based on speed over time
    }

    public void SetRotateLock(bool isLocked) //To be called by collision scripts later
    {
        rotateLock = isLocked;
    }
}
