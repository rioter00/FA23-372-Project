using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public void shoot(GameObject arrowPrefab, Transform arrowShootPoint, float arrowSpeed)
    {
        var Arrow = Instantiate(arrowPrefab, arrowShootPoint.position, arrowShootPoint.rotation);
        Arrow.GetComponent<Rigidbody>().velocity = arrowShootPoint.forward * arrowSpeed;
        //
    }
}
