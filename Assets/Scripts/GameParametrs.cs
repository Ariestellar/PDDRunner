using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameParametrs 
{
    public enum SignPriorityWay//Чем незначительнее число, тем менее значительный приоритет
    {
        unsigned = 0,
        minor = 1,
        minorRightTurn = 2,
        minorLeftTurn = 3,
        mainRightTurn = 4,
        mainLeftTurn = 5,
        main = 6,
    }

    public enum PriorityStatus
    {
        nonPriority,
        priority,
    }

    public enum VehicleDirection
    {
        left = 0,
        right = 1,
        straight = 2
    }
        
    public enum RelativePositionCars
    {
        east = 0,
        nord = 1,
        west = 2,
        player = 3
    }
}
