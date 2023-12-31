using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    [SerializeField] float lifeTime = 5f;

    private void OnEnable() {
        Invoke("Disable", lifeTime); //Hides bullet after 5 seconds
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ground")) { //Hides bullet if it touches anything with the Ground tag
            Disable();
        }
        else if (other.CompareTag("Enemy")) { //Checks to see if it hit an enemy then damages it acordingly
            other.GetComponentInParent<EnemyHealth>().TakeDamage(1);
        }
        else if(other.CompareTag("Dummy")){
            other.GetComponentInParent<DummyHealth>().TakeDamage(1);
        }
    }

    private void Disable() {
        gameObject.SetActive(false);
    }
}
