using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _trackingDistance; 

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _target.transform.position.z - _trackingDistance);
    }
}
