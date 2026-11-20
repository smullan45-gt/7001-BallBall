using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private Transform cam;
    private CharacterController charCon;

    public float speed;
    public float turnSpeed;
    public float gravity = -9.8f;

    private Vector3 finalDirection;

    public InputAction moveAction;
    public InputAction batAction;

    private bool batSwinging = false;

    private Vector2 moveInput;

    public GameObject bat;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
        batAction.Enable();
        charCon = GetComponent<CharacterController>();
        cam = Camera.main.transform; //Cam now equals camera's position and rotation
    }

    // Update is called once per frame
    void Update()
    {
        CheckMoveInput();
        CheckBatInput();
    }

    void CheckMoveInput()
    {
        Vector3 camDirection = CalculateCameraDirection();
        finalDirection = CalculatePlayerDirection(camDirection);

        if (finalDirection != Vector3.zero) 
        {
            RotatePlayer(finalDirection);
        }
        MoveWithGravity(finalDirection);
        
    }

    Vector3 CalculateCameraDirection()
    {
        Vector3 camPosition = new Vector3(cam.position.x, transform.position.y, cam.position.z); //Setting camera positon with player Y to prevent issues with downward rotated camera
        return (transform.position - camPosition).normalized; //Find direction camera is pointing at to decide what forward will be on input
        //Target direction = Target Position - Origin Position
    }

    Vector3 CalculatePlayerDirection(Vector3 camDirection)
    {
        moveInput = moveAction.ReadValue<Vector2>();
        Vector3 forwardMovement = camDirection * moveInput.y;
        Vector3 horizontalMovement = cam.right * moveInput.x;
        return Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1); //Clamp, or limit, the vector into a 1 -1 range
    }

    void RotatePlayer(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up); //Sets the direction to look, based on player input, rotating around Y axis
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime); //Smooth rotation based on speed over time
    }

    void MoveWithGravity(Vector3 direction)
    {
        direction.y += gravity * Time.deltaTime;
        charCon.Move(direction * speed * Time.deltaTime);
    }

    void CheckBatInput()
    {
        if(batAction.triggered && batSwinging == false)
        {
            Debug.Log("Bat been swung!");
            batSwinging = true;
            StartCoroutine(BatSwing());
        }
    }

    IEnumerator BatSwing()
    {
        bat.tag = "Bat";
        bat.GetComponent<Animator>().Play("BatSwing");
        yield return new WaitForSeconds(0.66f);
        bat.GetComponent<Animator>().Play("New State");
        bat.tag = "Untagged";
        batSwinging = false;
    }

    public Vector3 GetPlayerDirection()
    {
        return finalDirection;
    }
}