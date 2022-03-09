using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform PlayerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] bool LockCursor = true;
    [SerializeField] float MovementSpeed = 6.0f;
    [SerializeField] float Gravity = -13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float MoveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float MouseSmoothTime = 0.03f;
    [SerializeField] bool canmove = true;

    float CameraPitch = 0.0f;
    float VelocityY = 0.0f;
    CharacterController Controller = null;

    Vector2 CurrentDirection = Vector2.zero;
    Vector2 CurrentDirVelocity = Vector2.zero;

    Vector2 CurrentMouseDelta = Vector2.zero;
    Vector2 CurrentMouseDeltaVelocity = Vector2.zero;

    public void Start()
    {
        canmove = true;
        Controller = GetComponent<CharacterController>();
        if (LockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void stop()
    {
        canmove = false;
    }

    public void unlock()
    {
        Controller = GetComponent<CharacterController>();
        if (LockCursor)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook()
    {
        if (canmove == true)
        {
            Vector2 TargetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            CurrentMouseDelta = Vector2.SmoothDamp(CurrentMouseDelta, TargetMouseDelta, ref CurrentMouseDeltaVelocity, MouseSmoothTime);

            CameraPitch -= CurrentMouseDelta.y * mouseSensitivity;
            CameraPitch = Mathf.Clamp(CameraPitch, -75.0f, 75.0f);

            PlayerCamera.localEulerAngles = Vector3.right * CameraPitch;

            transform.Rotate(Vector3.up * CurrentMouseDelta.x * mouseSensitivity);
        }
    }

    void UpdateMovement()
    {
        if(canmove == true){

            Vector2 TargetDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            TargetDirection.Normalize();

            CurrentDirection = Vector2.SmoothDamp(CurrentDirection, TargetDirection, ref CurrentDirVelocity, MoveSmoothTime);

            if (Controller.isGrounded)
                VelocityY = 0.0f;

            VelocityY += Gravity * Time.deltaTime;


            Vector3 Velocity = (transform.forward * CurrentDirection.y + transform.right * CurrentDirection.x) * MovementSpeed + Vector3.up * VelocityY;

            Controller.Move(Velocity * Time.deltaTime);
        }
    }
}
