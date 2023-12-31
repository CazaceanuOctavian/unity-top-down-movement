using System;
using System.Collections;
using System.Collections.Generic;
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
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       movePlayer();

       if(Input.GetMouseButton(1)) {
            rotatePlayerToMouse();
       }
    }

    void movePlayer () {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * walkModifier * Time.deltaTime;
        
        //transform.Translate(movementDirection * speedVar * Time.deltaTime, Space.World);

        if(rb.velocity != Vector3.zero && !Input.GetMouseButton(1)) {
            Quaternion toRotation = Quaternion.LookRotation(rb.velocity, Vector3.up);

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
