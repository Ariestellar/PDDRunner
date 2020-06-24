using GameParametrs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementTraffic : MonoBehaviour
{
    [SerializeField] private List<Route> _routes;
    [SerializeField] private List<Car> _currentCarsAtCrossroad;

    [SerializeField] private List<Car> _firstQueue;
    [SerializeField] private List<Car> _secondQueue;
    [SerializeField] private List<Car> _thirdQueue;
    [SerializeField] private List<Car> _fourthQueue;

    [SerializeField] private List<List<Car>> _queues;
    [SerializeField] private int _numberCurrentQueue;
    [SerializeField] private bool _CarsMainIntersectionTest;

    [SerializeField] private GameObject _tempTimer;
    [SerializeField] private Action _queueWent;

    private void Awake()
    {
        _queueWent += StartQueue;
        _queues = new List<List<Car>>() { _firstQueue, _secondQueue, _thirdQueue, _fourthQueue };
    }
    public void Go(List<Car> cars)
    {
        _currentCarsAtCrossroad = cars;        
        InitializaceDirectionMovement(_currentCarsAtCrossroad);
        Prioritization(_currentCarsAtCrossroad);
        
        StartMovement();
    }

    /*
     * Инициализируем маршрут движения для каждой текущей машины, проверять пересечения маршрутов только после нее     
     */
    private void InitializaceDirectionMovement(List<Car> cars)
    {
        foreach (var car in cars)
        {            
            _routes[(int)car.relativePositionCars].Init(car);
        }
    }

    private void Prioritization(List<Car> cars)
    {
        //задать уровни приоритета (на главной дороге может быть две машины которые едут поочередно)

        _CarsMainIntersectionTest = IsCarsMainIntersection(cars);
        for (int currentCarNumber = 0; currentCarNumber < cars.Count; currentCarNumber++)
        {
            CheckIntersections(currentCarNumber, cars);
        }
    }


    private void CheckIntersections(int currentCarNumber, List<Car> carsAtCrossroad)
    {
        int numberIntersections = 0;
                
        for (int carNumberAtCrossroad = 0; carNumberAtCrossroad < carsAtCrossroad.Count; carNumberAtCrossroad++)
        {
            if (currentCarNumber != carNumberAtCrossroad)
            {
                if (carsAtCrossroad[currentCarNumber].signValue == SignPriorityWay.unsigned)
                {
                    if (carsAtCrossroad[currentCarNumber].direction == VehicleDirection.straight || carsAtCrossroad[currentCarNumber].direction == VehicleDirection.left)
                    {
                        if (carsAtCrossroad[currentCarNumber].boundRoute.Intersects(carsAtCrossroad[carNumberAtCrossroad].boundRoute))
                        {
                            if (IsHindranceOnRight(carsAtCrossroad[currentCarNumber].relativePositionCars, carsAtCrossroad[carNumberAtCrossroad].relativePositionCars))
                            {
                                numberIntersections += 1;
                            }
                            else if (IsOncomingTraffic(carsAtCrossroad[currentCarNumber].relativePositionCars, carsAtCrossroad[carNumberAtCrossroad].relativePositionCars))
                            {
                                numberIntersections += 1;
                            }
                        }
                    }
                }
                else if(carsAtCrossroad[currentCarNumber].signValue == SignPriorityWay.main || carsAtCrossroad[currentCarNumber].signValue == SignPriorityWay.minor)
                {
                    if (carsAtCrossroad[currentCarNumber].boundRoute.Intersects(carsAtCrossroad[carNumberAtCrossroad].boundRoute))
                    {
                        if (carsAtCrossroad[currentCarNumber].signValue == SignPriorityWay.main)
                        {
                            if (IsOncomingTraffic(carsAtCrossroad[currentCarNumber].relativePositionCars, carsAtCrossroad[carNumberAtCrossroad].relativePositionCars) && carsAtCrossroad[currentCarNumber].direction == VehicleDirection.left)
                            {
                                numberIntersections += 1;
                            }
                        }
                        else if(carsAtCrossroad[currentCarNumber].signValue == SignPriorityWay.minor)
                        {
                            if (IsOncomingTraffic(carsAtCrossroad[currentCarNumber].relativePositionCars, carsAtCrossroad[carNumberAtCrossroad].relativePositionCars) && carsAtCrossroad[carNumberAtCrossroad].direction == VehicleDirection.left)
                            {
                                //учитываем все пересечения, кроме случая когда встречная машина поворачивает налево
                            }
                            else if(carsAtCrossroad[carNumberAtCrossroad].signValue == SignPriorityWay.main && _CarsMainIntersectionTest)//если главные дороги пересекаются
                            {                                
                                numberIntersections += 2;
                            }
                            else
                            {
                                numberIntersections += 1;
                            }
                        }                        
                    }                    
                }
                else
                {
                    if (carsAtCrossroad[currentCarNumber].boundRoute.Intersects(carsAtCrossroad[carNumberAtCrossroad].boundRoute))
                    {
                        if (carsAtCrossroad[currentCarNumber].signValue < carsAtCrossroad[carNumberAtCrossroad].signValue)
                        {
                            if (carsAtCrossroad[currentCarNumber].signValue < SignPriorityWay.mainRightTurn && (carsAtCrossroad[carNumberAtCrossroad].signValue == SignPriorityWay.mainRightTurn || carsAtCrossroad[carNumberAtCrossroad].signValue == SignPriorityWay.mainLeftTurn) && _CarsMainIntersectionTest)//главные дороги пересекаються
                            {
                                numberIntersections += 2;
                            }
                            else
                            {
                                numberIntersections += 1;
                            }                            
                        }
                    }
                }
            }
        }
        SetQueues(numberIntersections, carsAtCrossroad[currentCarNumber]);       
    }

    private bool IsCarsMainIntersection(List<Car> carsAtCrossroad)//пересекаются ли автомобили на главной дороге?
    {
        List<Car> carsMainRoad = new List<Car>();
        foreach (var car in carsAtCrossroad)
        {
            if ((int)car.signValue >= (int)SignPriorityWay.mainRightTurn)
            {
                carsMainRoad.Add(car);
            }
        }

        if (carsMainRoad.Count > 1)
        {
            if (carsMainRoad[0].boundRoute.Intersects(carsMainRoad[1].boundRoute))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }      
    }

    private void SetQueues(int numberIntersections, Car currentCar)
    {
        if (numberIntersections == 0)
        {
            _firstQueue.Add(currentCar);
        }
        else
        {
            if (numberIntersections == 1)
            {
                _secondQueue.Add(currentCar);
            }
            else if (numberIntersections == 2)
            {
                _thirdQueue.Add(currentCar);
            }
            else
            {
                if (_thirdQueue.Count != 0)
                {
                    _fourthQueue.Add(currentCar);
                }
                else
                {
                    _thirdQueue.Add(currentCar);
                }                
            }
        }
    }

    private bool IsHindranceOnRight(RelativePositionCars currentCar, RelativePositionCars carAtCrossroad)
    {
        if ((currentCar == RelativePositionCars.player && carAtCrossroad == RelativePositionCars.east) || (currentCar == RelativePositionCars.east && carAtCrossroad == RelativePositionCars.nord)
           || (currentCar == RelativePositionCars.nord && carAtCrossroad == RelativePositionCars.west) || (currentCar == RelativePositionCars.west && carAtCrossroad == RelativePositionCars.player))
        {
            return true;
        }
        else
        {
            return false;
        }        
    }

    private bool IsOncomingTraffic(RelativePositionCars currentCar, RelativePositionCars carAtCrossroad)
    {
        if ((currentCar == RelativePositionCars.player && carAtCrossroad == RelativePositionCars.nord) || (currentCar == RelativePositionCars.east && carAtCrossroad == RelativePositionCars.west)
           || (currentCar == RelativePositionCars.nord && carAtCrossroad == RelativePositionCars.player) || (currentCar == RelativePositionCars.west && carAtCrossroad == RelativePositionCars.east))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StartMovement()
    {
        for (int i = 0; i < _queues.Count; i++)
        {
            GameObject timer = Instantiate(_tempTimer);
            timer.GetComponent<Timer>().StartTimer(i + 0.8f, _queueWent);
        }                
    }

    private void StartQueue()
    {
        foreach (var car in _queues[_numberCurrentQueue])
        {
            car.MovementAtCrossroad.SetMove(true);
        }
        _numberCurrentQueue += 1;
    }
}
