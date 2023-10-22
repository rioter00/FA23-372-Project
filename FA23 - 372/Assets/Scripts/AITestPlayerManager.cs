using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int defaultHealthPoints;
    private int damagePerHit;
    private int healthPoints;
    private MeshRenderer meshRenderer;
    Color origColor;
    float flashTime = .15f;
    // Start is called before the first frame update
    void Awake()
    {
        healthPoints = defaultHealthPoints;
        meshRenderer = GetComponent<MeshRenderer>();
        origColor = meshRenderer.material.color;
    }

    public bool TakeHit(int damage) {
        damagePerHit = damage;
        FlashStart();
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        
        healthPoints -= damagePerHit;
        //gameObject.GetComponent<Renderer>().material.color = Color.black;
        bool isDead = healthPoints <= 0;
        if (isDead) {
            Die();
        }
        return isDead;
    }

    private void Die() {
        Destroy(gameObject);
    }

    void FlashStart() {
        meshRenderer.material.color = Color.red;
        Invoke("FlashStop", flashTime);
    }

    void FlashStop() {
        meshRenderer.material.color = origColor;
    }    
    // Update is called once per frame
    void Update()
    {
        
    }
}
