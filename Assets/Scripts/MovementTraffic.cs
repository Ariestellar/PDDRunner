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
    [SerializeField] private GameObject _tempTimer;

    private List<List<Car>> _queues;
    private Action _queueWent;
    private int _numberCurrentQueue;

    private void Awake()
    {
        _queueWent += StartQueue;
        _queues = new List<List<Car>>() { _firstQueue, _secondQueue, _thirdQueue, _fourthQueue };
    }

    public void Go(List<Car> cars)//Когда наступает событие, игрок уже должен быть в списке машин
    {
        _currentCarsAtCrossroad = cars;        
        InitializaceDirectionMovement(_currentCarsAtCrossroad);//Машинам заданы маршруты движения, по их уже заранее заданным направлениям, после можно регулировать движение

        
        Prioritization(_currentCarsAtCrossroad);        
        StartMovement();
    }
    
    //Инициализируем маршрут движения для каждой текущей машины, проверять пересечения маршрутов только после нее 
    private void InitializaceDirectionMovement(List<Car> cars)
    {
        foreach (var car in cars)
        {            
            _routes[(int)car.relativePositionCars].Init(car);
        }
    }

    private void Prioritization(List<Car> cars)
    {
        if (cars[0].signValue == SignPriorityWay.unsigned)//Для равнозначных дорог
        {
            for (int i = 0; i < cars.Count; i++)
            {
                SetQueue(cars[i], cars[(i + 1) % cars.Count], cars[(i + 2) % cars.Count]);
            }            
        }
        else 
        {
            //Задать уровни приоритета (на главной дороге может быть две машины которые едут поочередно или вместе, так как и на второстепенной)
            List<Car> mainGroupRoad = GetRoadGroup(cars, SignPriorityWay.main); //определить группу главных дорог(приоритетных) 
            List<Car> minorGroupRoad = GetRoadGroup(cars, SignPriorityWay.minor); //определить группу второстепенных дорог
            if (mainGroupRoad.Count == 0)
            {
                SetQueue(minorGroupRoad, ref _firstQueue, ref _secondQueue);
            }
            else
            {
                SetQueue(mainGroupRoad, ref _firstQueue, ref _secondQueue);//Первая и вторая очереди выделенны для машин с приритетных дорог, Вторая и третья очереди для машин со второстепенных дорог
                if (minorGroupRoad.Count != 0)
                {
                    if (_secondQueue.Count == 0)
                    {
                        SetQueue(minorGroupRoad, ref _secondQueue, ref _thirdQueue);//Задаем очереди для каждой группы машин
                    }
                    else
                    {
                        SetQueue(minorGroupRoad, ref _thirdQueue, ref _fourthQueue);
                    }
                }                               
            }            
        }        
    }

    private void SetQueue(List<Car> groupRoad, ref List<Car> firstRelativeQueue, ref List<Car> secondRelativeQueue)//Рассматриваем очереди погруппно, всего 2 элемента в массиве
    {
        if (groupRoad.Count > 1)
        {
            if (groupRoad[0].boundRoute.Intersects(groupRoad[1].boundRoute))
            {
                if (IsHindranceOnRight(groupRoad[0].relativePositionCars, groupRoad[1].relativePositionCars) || (IsOncomingTraffic(groupRoad[0].relativePositionCars, groupRoad[1].relativePositionCars) && groupRoad[0].direction == VehicleDirection.left))
                {
                    firstRelativeQueue.Add(groupRoad[1]);
                    secondRelativeQueue.Add(groupRoad[0]);
                }
                else
                {
                    firstRelativeQueue.Add(groupRoad[0]);
                    secondRelativeQueue.Add(groupRoad[1]);
                }
            }
            else//если не пересекакются, то в одну очередь, едут одновременно
            {
                firstRelativeQueue.Add(groupRoad[0]);
                firstRelativeQueue.Add(groupRoad[1]);
            }
        }
        else
        {
            firstRelativeQueue.Add(groupRoad[0]);
        }
    }

    private void SetQueue(Car currentCar, Car firstCar, Car secondCar)//Для равнозначных дорог исключая 4 машины на перекрестке, т.к. они едут по договоренности и машину поворачивающую на лево т.к. возможность выезда на центрперекрестка и остановка не реализованна
    {        
        if (currentCar.direction == VehicleDirection.right)
        {
            _firstQueue.Add(currentCar);
        }
        else
        {
            if ((IsHindranceOnRight(currentCar.relativePositionCars, firstCar.relativePositionCars) || IsOncomingTraffic(currentCar.relativePositionCars, firstCar.relativePositionCars)) && (IsHindranceOnRight(currentCar.relativePositionCars, secondCar.relativePositionCars) || IsOncomingTraffic(currentCar.relativePositionCars, secondCar.relativePositionCars)))
            {
                _thirdQueue.Add(currentCar);
            }
            else if ((IsOncomingTraffic(currentCar.relativePositionCars, firstCar.relativePositionCars)) || (IsOncomingTraffic(currentCar.relativePositionCars, secondCar.relativePositionCars)))
            {
                _firstQueue.Add(currentCar);
            }
            else if ((IsHindranceOnRight(currentCar.relativePositionCars, firstCar.relativePositionCars)) || (IsHindranceOnRight(currentCar.relativePositionCars, secondCar.relativePositionCars)))
            {
                _secondQueue.Add(currentCar);
            }
        }        
    }

    private List<Car> GetRoadGroup(List<Car> carsAtCrossroad, SignPriorityWay groupPriorityWay)
    {
        List<Car> carsGroupRoad = new List<Car>();
        foreach (var car in carsAtCrossroad)
        {
            if (groupPriorityWay == SignPriorityWay.main && (int)car.signValue >= (int)SignPriorityWay.mainRightTurn)
            {
                carsGroupRoad.Add(car);
            }
            else if(groupPriorityWay == SignPriorityWay.minor && (int)car.signValue <= (int)SignPriorityWay.minorLeftTurn)
            {
                carsGroupRoad.Add(car);
            }
        }
        return carsGroupRoad;
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
            timer.GetComponent<Timer>().StartTimer(i + 0.5f, _queueWent);            
        }                
    }

    private void StartQueue()//this -> Action _queueWent
    {
        foreach (var car in _queues[_numberCurrentQueue])
        {
            car.movementAtCrossroad.SetMove(true);
        }
        _numberCurrentQueue += 1;
    }
}
