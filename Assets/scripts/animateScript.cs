using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateScript : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)) {
            animator.SetBool("WALK00_B", true);
        }
    }
}
