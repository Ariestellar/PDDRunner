using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    [SerializeField] private Bounds _boundRoute;
    [SerializeField] private PointMovement _movementAtCrossroad;
    
    public PointMovement movementAtCrossroad => _movementAtCrossroad;
    public Bounds boundRoute => _boundRoute;
    public VehicleDirection direction => _vehicleDirection;
    public SignPriorityWay signValue => _signValue;
    public RelativePositionCars relativePositionCars => _relativePositionCars;
    public PriorityStatus priorityStatus => _priorityStatus;
    

    public void Init(VehicleDirection vehicleDirection, RelativePositionCars relativePositionCars, SignPriorityWay signValue)
    {
        _movementAtCrossroad = GetComponent<PointMovement>();
        _signValue = signValue;
        _relativePositionCars = relativePositionCars;
        _vehicleDirection = vehicleDirection;
        EnableTurnSignalCar(vehicleDirection);        
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

    public void SetCarRoute(Bounds boundRoute)
    {
        _boundRoute = boundRoute;
    }    
}
