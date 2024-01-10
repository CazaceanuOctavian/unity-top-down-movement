using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Gun[] LoadOut;
    public Transform weaponParent;
    private GameObject currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        Equip(0); //daca apas 1 pe tastatura se echipeaza arma 0;
    }

    void Equip (int p_ind)
    {
        if (currentWeapon != null) Destroy(currentWeapon);

        GameObject t_newEquipment = Instantiate(LoadOut[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        t_newEquipment.transform.localPosition = Vector3.zero;
        t_newEquipment.transform.localEulerAngles = Vector3.zero;

        currentWeapon = t_newEquipment;
    }

}
