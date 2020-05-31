using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameParametrs 
{
    public enum SignPriorityWay
    {
        unsigned = 0,
        main = 1,
        minor = 2,
        mainRightTurn = 3,
        mainLeftTurn = 4,
        minorRightTurn = 5,
        minorLeftTurn = 6,
    }

    public enum PriorityStatus
    {
        nonPriority,
        priority,
    }

    public enum VehicleDirection
    {
        left,
        right,
        straight
    }
}
