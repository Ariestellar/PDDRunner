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
        for (int i = 0; i < cars.Count; i++)
        {
            CheckIntersections(i, cars);
        }
    }


    private void CheckIntersections(int currentCarNumber, List<Car> carsAtCrossroad)
    {
        int numberIntersections = 0;
        for (int carNumberAtCrossroad = 0; carNumberAtCrossroad < carsAtCrossroad.Count; carNumberAtCrossroad++)
        {
            if (currentCarNumber != carNumberAtCrossroad)
            {
                if (carsAtCrossroad[currentCarNumber].boundRoute.Intersects(carsAtCrossroad[carNumberAtCrossroad].boundRoute))
                {                    
                    if (carsAtCrossroad[currentCarNumber].signValue < carsAtCrossroad[carNumberAtCrossroad].signValue)
                    {
                        numberIntersections += 1;
                    }
                    else if(carsAtCrossroad[currentCarNumber].signValue == carsAtCrossroad[carNumberAtCrossroad].signValue)// для равнозначных дорог
                    {
                        if (carsAtCrossroad[currentCarNumber].direction == VehicleDirection.left )
                        {
                            numberIntersections += 1;
                        }                        
                    }
                }                
            }            
        }

        if (numberIntersections == 0)
        {
            _firstQueue.Add(carsAtCrossroad[currentCarNumber]);
        }
        else
        {
            if (numberIntersections == 1)
            {
                _secondQueue.Add(carsAtCrossroad[currentCarNumber]);
            }
            else if(numberIntersections == 2)
            {
                _thirdQueue.Add(carsAtCrossroad[currentCarNumber]);
            }
            else if (numberIntersections == 3)
            {
                _fourthQueue.Add(carsAtCrossroad[currentCarNumber]);
            }
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
