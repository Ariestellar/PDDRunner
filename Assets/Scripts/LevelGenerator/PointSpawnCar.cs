using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawnCar : MonoBehaviour
{
    [Range(0f, 100f)] [SerializeField] private int _spawnChanceOnPoint;//Указывать в процентах, изменяется для корректировки сложности
    [SerializeField] private RelativePositionCars _relativePositionCars;
    private SignPriorityWay _signValue;    
    
    public SignPriorityWay signValue => _signValue;
    public int spawnChanceOnPoint => _spawnChanceOnPoint;
    public RelativePositionCars relativePositionCars => _relativePositionCars;

    public void SetSignValue(SignPriorityWay signValue)
    {
        _signValue = signValue;        
    }
}
