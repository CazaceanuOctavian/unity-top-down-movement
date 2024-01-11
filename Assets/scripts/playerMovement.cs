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
    private float goUp;
    
    //--> TODO REFACTOR SCRIPT (but works for now)
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        gatherInput();
        movePlayerSkewed();
        if(Input.GetMouseButton(1)) {
            rotatePlayerToMouse();
        }
    }

    void gatherInput() {
        _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    //movement to be used with ortographic camera
    void movePlayerSkewed() {
        if(_input != Vector3.zero) {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

            var skewedInput = matrix.MultiplyPoint3x4(_input);

            //if the player is not aiming and is not sprinting
            if(!Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftShift)) {
                rb.MovePosition(transform.position + skewedInput.normalized  * walkModifier * Time.deltaTime);
                //rotate twoards the position that the player is facing
                Quaternion toRotation = Quaternion.LookRotation(skewedInput, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            }
            //if the player is aiming don't rotate because the player model is already rotating twoards mouse
            else if (Input.GetMouseButton(1)) {
                rb.MovePosition(transform.position + skewedInput.normalized  * slowWalkModifier * Time.deltaTime);
            }
            //if the player is sprinting 
            else {
                rb.MovePosition(transform.position + skewedInput.normalized  * sprintModifier * Time.deltaTime);
                //rotate twoards the position that the player is facing
                 Quaternion toRotation = Quaternion.LookRotation(skewedInput, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void rotatePlayerToMouse () {
        Vector3 mouseInput = Input.mousePosition;
        Ray ray = cursorCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) {
            //Debug.DrawRay(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z), new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position);
            var hitObject = hit.transform.gameObject.GetComponent<Collider>();
            if(hit.collider == hitObject) {
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionStay(Collision other) {
        var yAxis = transform.position.y;
        if(other.transform.gameObject.tag != "floor" && Input.anyKey) {
            Debug.Log("whatamaidoing");
            timer();
            transform.Translate(0, 0.5f, 0); 
        }
        if(this.transform.position.y < yAxis) 
            return;
    }

    IEnumerator timer() {
        yield return new WaitForSeconds(1f);
    }
}
