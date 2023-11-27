using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageControllerTest : MonoBehaviour
{
    [SerializeField] private float damage = 10.0f;

    [SerializeField] private HealthControllerTest _healthController = null;

    public void Damage()
    {
        _healthController.currentPlayerHealth -= damage;
        _healthController.TakeDamage();
    }
}
