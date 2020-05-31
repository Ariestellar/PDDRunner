using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInTraffic : MonoBehaviour
{    
    [SerializeField] private Backlight _backlight;
    [SerializeField] private GameObject _turnSignalRight;
    [SerializeField] private GameObject _turnSignalLeft;
    [SerializeField] private PriorityStatus _priorityStatus;
    [SerializeField] private VehicleDirection _vehicleDirection;

    [SerializeField] private int _sequenceCars;
    public int SequenceCars => _sequenceCars;
    public PriorityStatus PriorityStatus => _priorityStatus;
    public VehicleDirection Direction => _vehicleDirection;

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

    public void SetSequenceCars(int sequenceCars)
    {
        _sequenceCars = sequenceCars;
    }

    public PriorityStatus GetPriorityStatus()
    {
        return _priorityStatus;
    }
}
