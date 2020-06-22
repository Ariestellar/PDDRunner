using GameParametrs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Route : MonoBehaviour
{
    //Перенести параметры из класса Car сюда, которые не относятся к машине а относяться к маршруту
    //Временное решение. Рефакторить, можно применить паттерн
    [SerializeField] private List<Transform> _pointsMovementRight;
    [SerializeField] private List<Transform> _pointsMovementStraight;
    [SerializeField] private List<Transform> _pointsMovementLeft;
    [SerializeField] private List<GameObject> _sidesMovement;
    [SerializeField] private List<BoxCollider> _colliderSides;
    [SerializeField] private SignPriorityWay _signValue;//дублирующий параметр "Car"
    [SerializeField] private Car _car;

    private List<List<Transform>> _pointsMovement;
    private BoxCollider _colliderPointsMovement;
    private Bounds _bound;

    public SignPriorityWay SignValue => _signValue;

    private void Start()
    {
        _pointsMovement = new List<List<Transform>>() { _pointsMovementLeft, _pointsMovementRight, _pointsMovementStraight };        
    }

    public void Init(Car car)
    {
        _car = car;
        _signValue = car.SignValue;
        VehicleDirection vehicleDirection = _car.Direction;
        _sidesMovement[(int)vehicleDirection].SetActive(true);

        List<Transform>  currentPoint = _pointsMovement[(int)vehicleDirection];//точки текущего направления для NavMesh
        _colliderPointsMovement  = _colliderSides[(int)vehicleDirection];//коллайдер текущего направления для определения пересечений
        _bound = _colliderPointsMovement.bounds;//Границы коллайдера
    }

    public Bounds GetBoundMovement()
    {
        return _bound;
    }
}
