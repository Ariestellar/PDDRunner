using GameParametrs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Route : MonoBehaviour
{    
    [SerializeField] private List<Transform> _pointsMovementRight;
    [SerializeField] private List<Transform> _pointsMovementStraight;
    [SerializeField] private List<Transform> _pointsMovementLeft;
    [SerializeField] private List<GameObject> _sidesMovement;
    [SerializeField] private List<BoxCollider> _colliderSides;    

    private List<List<Transform>> _pointsMovement;
    private BoxCollider _colliderPointsMovement;
    

    private void Awake()
    {
        _pointsMovement = new List<List<Transform>>() { _pointsMovementLeft, _pointsMovementRight, _pointsMovementStraight };        
    }

    public void Init(Car car)
    {        
        VehicleDirection vehicleDirection = car.direction;
        _sidesMovement[(int)vehicleDirection].SetActive(true);//включаем необходимые точки и коллайдер

        car.movementAtCrossroad.SetPointsMovement(_pointsMovement[(int)vehicleDirection]);//задаем точки текущего направления для NavMesh
        _colliderPointsMovement  = _colliderSides[(int)vehicleDirection];//коллайдер текущего направления для определения пересечений
        car.SetCarRoute(_colliderPointsMovement.bounds);//Границы коллайдера
    }
}
