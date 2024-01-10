using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12;
    Vector3 velocity;

    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    public float JumpHeight = 3f;
    public float SprintModifier = 2;

    bool isGrounded;

    public Camera normalCam;
    private float BaseFOV;
    private float SprintFOV_Modifier = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        BaseFOV = normalCam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool Sprint = Input.GetKey(KeyCode.LeftShift);

        bool isSprinting = Sprint && z > 0 && isGrounded; // z > 0 detecteaza daca mergem in fata ca sa nu dam sprint in spate;

        float z_AdjustSpeed = speed;    
        if (isSprinting) z_AdjustSpeed *= SprintModifier;

        

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * z_AdjustSpeed * Time.deltaTime);

        

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); //formula din fizica este y=1/2g * t*t;
        if (isSprinting)
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, BaseFOV * SprintFOV_Modifier, Time.deltaTime * 8f);
        }
        else
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, BaseFOV, Time.deltaTime * 8f);
        }
    }
}
