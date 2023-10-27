using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    //camera always moves with the player
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
