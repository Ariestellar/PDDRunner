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

    private List<List<Transform>> _pointsMovement;
    private BoxCollider _colliderPointsMovement;
    

    private void Start()
    {
        _pointsMovement = new List<List<Transform>>() { _pointsMovementLeft, _pointsMovementRight, _pointsMovementStraight };        
    }

    public void Init(Car car)
    {        
        VehicleDirection vehicleDirection = car.direction;
        _sidesMovement[(int)vehicleDirection].SetActive(true);

        //List<Transform>  currentPoint = _pointsMovement[(int)vehicleDirection];//точки текущего направления для NavMesh
        _colliderPointsMovement  = _colliderSides[(int)vehicleDirection];//коллайдер текущего направления для определения пересечений
        car.SetCarRoute(_colliderPointsMovement.bounds);//Границы коллайдера
    }
}
