using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunAim : MonoBehaviour
{

    public LayerMask LayerMask;
    public Camera mainCamera;
    public Transform Aimpoint;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out var hit,
                    Mathf.Infinity, LayerMask))
            {
                Aimpoint.LookAt(hit.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * 1000);
    }
}
