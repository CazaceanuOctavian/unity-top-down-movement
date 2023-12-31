using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb; 
    private Transform playerTransform;

    [SerializeField] private float speedVar;
    [SerializeField] private float speedModifier;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationSpeed2;
    
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
       else {
            rotatePlayerToDirection();
       }
    }

    void movePlayer () {
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speedVar * Time.fixedDeltaTime;
        
        if(Input.GetKey(KeyCode.LeftShift)) {
            rb.velocity *= speedModifier * Time.fixedDeltaTime;
        }
        if(Input.GetMouseButton(1)) {
            rb.velocity /= speedModifier * Time.fixedDeltaTime;
        }
        
    }

    void rotatePlayerToMouse () {
        Vector3 mouseInput = Input.mousePosition;
        Debug.Log(mouseInput);
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseInput.x, mouseInput.y, 10f));
        Vector3 directionToMouse = targetPosition - transform.position;
        float angle = Mathf.Atan2(directionToMouse.x, directionToMouse.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
    }

    //TODO --> clean up code
    void rotatePlayerToDirection() {
        if(Input.GetKey(KeyCode.W)) {
            Quaternion zRotation = Quaternion.AngleAxis(0, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, zRotation, rotationSpeed2 * Time.fixedDeltaTime);
        }
        else if(Input.GetKey(KeyCode.S)) {
            Quaternion zRotation = Quaternion.AngleAxis(180, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, zRotation, rotationSpeed2 * Time.fixedDeltaTime);
        }
        else if(Input.GetKey(KeyCode.D)) {
            Quaternion zRotation = Quaternion.AngleAxis(90, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, zRotation, rotationSpeed2 * Time.fixedDeltaTime);
        }
        else if(Input.GetKey(KeyCode.A)) {
            Quaternion zRotation = Quaternion.AngleAxis(270, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, zRotation, rotationSpeed2 * Time.fixedDeltaTime);
        }
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) {
            Quaternion zRotation = Quaternion.AngleAxis(45, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, zRotation, rotationSpeed2 * Time.fixedDeltaTime);
        }
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) {
            Quaternion zRotation = Quaternion.AngleAxis(315, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, zRotation, rotationSpeed2 * Time.fixedDeltaTime);
        }
        if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) {
            Quaternion zRotation = Quaternion.AngleAxis(135, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, zRotation, rotationSpeed2 * Time.fixedDeltaTime);
        }
         if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) {
            Quaternion zRotation = Quaternion.AngleAxis(225, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, zRotation, rotationSpeed2 * Time.fixedDeltaTime);
        }
    }
}
