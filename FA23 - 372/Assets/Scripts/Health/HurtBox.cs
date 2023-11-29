using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour{
    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other) {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph != null) {
            ph.TakeDamage(damage);
        }
    }
}
