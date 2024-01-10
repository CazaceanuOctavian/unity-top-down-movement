using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    private AudioSource audioSource;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private float damage;
    private float nextTimeToFire = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire) {
            nextTimeToFire = Time.time + 1f/fireRate;
            shoot();
        }
        if(Input.GetMouseButton(1)) {
            laser.SetActive(true);
        }
        else
            laser.SetActive(false);
    }

    void shoot() {
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position + new Vector3(0, 1f, 0), this.transform.forward, out hit)) {
            Debug.Log(hit.transform.name);
            if(!audioSource.isPlaying)
                audioSource.Play();

            target currentTarget = hit.transform.GetComponent<target>();
            if(currentTarget!=null) {
                currentTarget.takeDamage(damage);
            }
        }
    }
}
