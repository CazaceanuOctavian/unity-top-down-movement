using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    [SerializeField] private float hp;

    public void takeDamage(float damageToBeTaken) {
        hp -= damageToBeTaken;
        if(hp<0f) {
            Destroy(gameObject);
        }
    }
}
