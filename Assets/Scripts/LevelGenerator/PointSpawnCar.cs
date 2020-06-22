using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawnCar : MonoBehaviour
{
    //Шансы указывать в процентах, изменяется для корректировки сложности
    [Range(0f, 100f)] [SerializeField] private int _spawnChanceOnPoint;
    /*[Range(0f, 100f)] [SerializeField] private int _chanceTurnRight;
    [Range(0f, 100f)] [SerializeField] private int _chanceTurnLeft;
    [Range(0f, 100f)] [SerializeField] private int _chanceTurnStraight;*/
    [SerializeField] private RelativePositionCars _relativePositionCars;
    private SignPriorityWay _signValue;    
    
    public SignPriorityWay signValue => _signValue;
    public int spawnChanceOnPoint => _spawnChanceOnPoint;
    public RelativePositionCars relativePositionCars => _relativePositionCars;

    //Для тестов
    [SerializeField] private VehicleDirection __carDirectionTest;

    public void SetSignValue(SignPriorityWay signValue)
    {
        _signValue = signValue;        
    }

    public VehicleDirection GetCarDirection()
    {
        return __carDirectionTest;
    }


}
