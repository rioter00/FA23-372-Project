using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{

    private int damage = 10;
    [SerializeField] PlayerHealth ph;

    void Update(){
        if (Input.GetKeyDown(KeyCode.P)) {
            ph.TakeDamage(damage);
        }
    }
}
