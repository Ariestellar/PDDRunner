using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawnCar : MonoBehaviour
{
    //Шансы указывать в процентах, изменяется для корректировки сложности
    [Range(0f, 100f)] [SerializeField] private int _spawnChanceOnPoint;    
    [SerializeField] private RelativePositionCars _relativePositionCars;
    [SerializeField] private SignPriorityWay _signValue;
    [SerializeField] private VehicleDirection __carDirection;

    public SignPriorityWay signValue => _signValue;
    public int spawnChanceOnPoint => _spawnChanceOnPoint;
    public RelativePositionCars relativePositionCars => _relativePositionCars;   

    public void SetSignValue(SignPriorityWay signValue)
    {
        _signValue = signValue;        
    }

    public VehicleDirection GetCarDirection()
    {
        return __carDirection;
    }

    private VehicleDirection GetRandomDirection(RelativePositionCars relativePositionCars)
    {
        int directionNumber;
        do
        {
            directionNumber = Random.Range(0, 3);
        } while ((relativePositionCars == RelativePositionCars.east && (VehicleDirection)directionNumber == VehicleDirection.right)
        || (relativePositionCars == RelativePositionCars.west && (VehicleDirection)directionNumber == VehicleDirection.left));
        return (VehicleDirection)directionNumber;
    }
}
