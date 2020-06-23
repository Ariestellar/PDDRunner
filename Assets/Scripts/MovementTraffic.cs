using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTraffic : MonoBehaviour
{
    [SerializeField] private List<Route> _routes;
    [SerializeField] private List<Car> _currentCarsAtCrossroad;

    [SerializeField] private List<Car> _firstQueue;
    [SerializeField] private List<Car> _secondQueue;
    [SerializeField] private List<Car> _thirdQueue;
    [SerializeField] private List<Car> _fourthQueue;

    public void Go(List<Car> cars)
    {
        _currentCarsAtCrossroad = cars;        
        InitializaceDirectionMovement(_currentCarsAtCrossroad);
        Prioritization(_currentCarsAtCrossroad);
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
        
    }
}
