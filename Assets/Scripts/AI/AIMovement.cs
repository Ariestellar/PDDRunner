using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{    
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;

    private bool _isMove;
    private bool _isRotate;

    private void FixedUpdate()
    {
        Move();        
    }
   
    public void OnMove()
    {
        _isMove = true;
        if (GetComponent<Car>().Direction == VehicleDirection.right)//решение в лоб, поворот направо
        {
            _isRotate = true;
        }
        else if (GetComponent<Car>().Direction == VehicleDirection.left)
        { 
        
        }

    }

    private void Move()
    {
        if (_isMove)
        {
            transform.Translate(new Vector3(1, 0.0f, 0.0f) * _speed * Time.fixedDeltaTime);//20
            if (_isRotate)
            {
                RotateRight();
            }
        }
    }

    private void RotateRight()
    {
        //корректно работает только для машины слева
        transform.Rotate(new Vector3(0.0f, 1, 0.0f) * _speedRotate * Time.fixedDeltaTime); //при 20 до 270 направо 
        if (transform.localEulerAngles.y > 90)
        {
            _isRotate = false;
        }        
    }    
}
