using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb; 
    private Transform playerTransform;
    [SerializeField] private Camera cursorCamera;
    [SerializeField] private float walkModifier;
    [SerializeField] private float sprintModifier;
    [SerializeField] private float slowWalkModifier;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float aimRoationSpeed;
    [SerializeField] Collider planeCollider;
    private Vector3 _input;
    
    //--> TODO REFACTOR SCRIPT (but works for now)
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       movePlayer();
       gatherInput();
       if(Input.GetMouseButton(1)) {
            rotatePlayerToMouse();
       }
    }

    void gatherInput() {
        _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    void movePlayer () {
        //used for setting character rotation twoards the direction that you are moving in --> walkModifer * Time.deltaTime may not be necessary
        Vector3 velocityPlaceholder = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * walkModifier * Time.deltaTime;
        
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        var skewedInput = matrix.MultiplyPoint3x4(_input);

        //movement to be used with isometric camera
        rb.MovePosition(transform.position + skewedInput.normalized  * walkModifier * Time.deltaTime);
        
        //TODO --> clean up if statement
        if(velocityPlaceholder != Vector3.zero && !Input.GetMouseButton(1)) {
            Quaternion toRotation = Quaternion.LookRotation(skewedInput, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.LeftShift)) {
            rb.velocity *= sprintModifier * Time.deltaTime;
        }
    
        if(Input.GetMouseButton(1)) {
            rb.velocity /= slowWalkModifier * Time.deltaTime;
        }
        
    }

    void rotatePlayerToMouse () {
        Vector3 mouseInput = Input.mousePosition;
        Ray ray = cursorCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) {
            if(hit.collider == planeCollider) {
                //TODO --> make the movement smooth
                //Quaternion rotation = Quaternion.LookRotation(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                //transform.rotation = rotation;
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }

    }
}
