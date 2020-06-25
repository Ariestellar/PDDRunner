using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawnCar : MonoBehaviour
{
    //Шансы указывать в процентах, изменяется для корректировки сложности
    [Range(0f, 100f)] [SerializeField] private int _spawnChanceOnPoint;    
    [SerializeField] private RelativePositionCars _relativePositionCars;    
    [SerializeField] private VehicleDirection _carDirection;

    private SignPriorityWay _signValue;
    public SignPriorityWay signValue => _signValue;
    public int spawnChanceOnPoint => _spawnChanceOnPoint;
    public RelativePositionCars relativePositionCars => _relativePositionCars;   

    public void SetSignValue(SignPriorityWay signValue)
    {
        _signValue = signValue;        
    }

    public VehicleDirection GetCarDirection()
    {
        return GetRandomDirection(_relativePositionCars, _signValue);
    }

    private VehicleDirection GetRandomDirection(RelativePositionCars relativePositionCars, SignPriorityWay signValue)
    {
        int directionNumber;
        do
        {
            if (signValue == SignPriorityWay.unsigned)//на равнозначных дорогах исключаем поворот на лево
            {
                directionNumber = Random.Range(1, 3);
            }
            else
            {
                directionNumber = Random.Range(0, 3);
            }
            
        } while ((relativePositionCars == RelativePositionCars.east && (VehicleDirection)directionNumber == VehicleDirection.right)//В игре машины не должны ехать в направлении игрока
        || (relativePositionCars == RelativePositionCars.west && (VehicleDirection)directionNumber == VehicleDirection.left));

        return (VehicleDirection)directionNumber;
    }
}
