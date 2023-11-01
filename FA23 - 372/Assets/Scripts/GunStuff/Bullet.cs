using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    [SerializeField] float lifeTime = 5f;

    private void Awake() {
        Destroy(gameObject, lifeTime); //Desroys bullet after 5 seconds
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ground")) { //Destroys bullet if it touches anything with the Ground tag
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy")) { //Checks to see if it hit an enemy then damages it acordingly
            other.GetComponentInParent<Health>().TakeDamage(1);
        }
    }
}
