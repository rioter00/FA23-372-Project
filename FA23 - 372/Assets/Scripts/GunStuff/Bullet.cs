using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    [SerializeField] float lifeTime = 5;

    private void Awake() {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Ground")) {
            Destroy(gameObject);
        }
    }
}
