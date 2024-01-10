using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new gun", menuName = "gun")]
public class Gun : ScriptableObject
{
    public string name;
    public GameObject prefab;
}
