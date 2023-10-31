using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] int maxHP;
    
}

public enum LivingState {
    ALIVE,
    DYING,
    DEAD,
}
