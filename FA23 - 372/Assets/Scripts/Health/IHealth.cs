using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth{
    public int HP { get;  set; }
    public int MaxHP { get; set; }
    public LivingState LState { get; set; }

    public void TakeDamage(int damage);

    public void Death();
}

public enum LivingState {
    ALIVE,
    DEAD,
}
