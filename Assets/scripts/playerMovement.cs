using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb; 
    private Transform playerTransform;

    [SerializeField] private float walkModifier;
    [SerializeField] private float speedModifier;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float aimRoationSpeed;

    
    
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
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * walkModifier * Time.deltaTime;
        
        //transform.Translate(movementDirection * speedVar * Time.deltaTime, Space.World);

        if(rb.velocity != Vector3.zero && !Input.GetMouseButton(1)) {
            Quaternion toRotation = Quaternion.LookRotation(rb.velocity, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.LeftShift)) {
            rb.velocity *= speedModifier * Time.deltaTime;
        }
        
        if(Input.GetMouseButton(1)) {
            rb.velocity /= speedModifier * Time.deltaTime;
        }
        
    }

    void rotatePlayerToMouse () {
        Vector3 mouseInput = Input.mousePosition;
        Debug.Log(mouseInput);
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseInput.x, mouseInput.y, 10f));
        Vector3 directionToMouse = targetPosition - transform.position;
        float angle = Mathf.Atan2(directionToMouse.x, directionToMouse.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, aimRoationSpeed * Time.fixedDeltaTime);
    }
}
