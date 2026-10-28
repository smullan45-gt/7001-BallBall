using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Transform cam;
    private CharacterController charCon;

    public float speed;
    public float turnSpeed;
    private float rotationY;

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
        charCon = GetComponent<CharacterController>();
        cam = Camera.main.transform; //Cam now equals camera's position and rotation
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
            //LockRotation();
            CheckMoveInput();
        }
    }

    void LockRotation()
    {
        rotationY = rigidBody.rotation.y;
        rigidBody.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    void CheckMoveInput()
    {
        Vector3 camPosition = new Vector3(cam.position.x, transform.position.y, cam.position.z); //Setting camera positon with player Y to prevent issues with downward rotated camera
        Vector3 direction = (transform.position - camPosition).normalized; //Find direction camera is pointing at to decide what forward will be on input
        //Target direction = Target Position - Origin Position

        moveInput = moveAction.ReadValue<Vector2>();
        Vector3 forwardMovement = direction * moveInput.y;
        Vector3 horizontalMovement = cam.right * moveInput.x;

        Vector3 finalMove = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1); //Clamp, or limit, the vector into a 1 -1 range
        charCon.Move(finalMove * speed * Time.deltaTime); //movement relative to camera, so must use world axis (camera is in world)

        Quaternion targetRotation = Quaternion.LookRotation(finalMove, Vector3.up); //Sets the direction to look, based on player input, rotating around Y axis
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime); //Smooth rotation based on speed over time
    }

    public void SetRotateLock(bool isLocked) //To be called by collision scripts later
    {
        rotateLock = isLocked;
    }
}
