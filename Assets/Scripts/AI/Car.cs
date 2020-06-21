using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{    
    [SerializeField] private Backlight _backlight;
    [SerializeField] private GameObject _turnSignalRight;
    [SerializeField] private GameObject _turnSignalLeft;
    [SerializeField] private PriorityStatus _priorityStatus;
    [SerializeField] private VehicleDirection _vehicleDirection;
    [SerializeField] private RelativePositionCars _relativePositionCars; 
    [SerializeField] private SignPriorityWay _signValue; 
    
    public PriorityStatus PriorityStatus => _priorityStatus;
    public VehicleDirection Direction => _vehicleDirection;

    public void Init(SignPriorityWay signValue, RelativePositionCars relativePositionCars)
    {
        _relativePositionCars = relativePositionCars;
        _signValue = signValue;
        _vehicleDirection = GetRandomDirection(_relativePositionCars);
        EnableTurnSignalCar(_vehicleDirection);
    }
    public void SetVehicleDirection(VehicleDirection vehicleDirection)
    {
        _vehicleDirection = vehicleDirection;
    }

    public void ShowHighlight()
    {
        _backlight.Show(true);
    }

    public void EnableTurnSignalCar(VehicleDirection vehicleDirection)
    {
        if (vehicleDirection == VehicleDirection.left)
            _turnSignalLeft.SetActive(true);
        else if (vehicleDirection == VehicleDirection.right)
            _turnSignalRight.SetActive(true);
    }

    public void SetPriorityStatus(PriorityStatus priority)
    {
        _priorityStatus = priority;
        _backlight.ChangeSignMoveCar(priority);
    }

    public PriorityStatus GetPriorityStatus()
    {
        return _priorityStatus;
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
