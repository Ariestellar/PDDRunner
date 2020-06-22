using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTraffic : MonoBehaviour
{
    [SerializeField] private List<Route> _routes;
    private List<Car> _currentCarsAtCrossroad;

    private List<Car> _firstQueue;
    private List<Car> _secondQueue;
    private List<Car> _thirdQueue;
    private List<Car> _fourthQueue;

    public void Go(List<Car> cars)
    {
        _currentCarsAtCrossroad = cars;        
        InitializaceDirectionMovement(cars);
    }

    /*
     * Инициализируем маршрут движения для каждой текущей машины, проверять пересечения маршрутов только после нее     
     */
    private void InitializaceDirectionMovement(List<Car> cars)
    {
        foreach (var car in cars)
        {
            _routes[(int)car.RelativePositionCars].Init(car);
        }
    }

    private void Prioritization()
    {
        foreach (var route in _routes)
        {
            for (int i = 0; i < _routes.Count; i++)
            {
                if (route.GetBoundMovement().Intersects(_routes[i].GetBoundMovement()))
                {
                    //route.SignValue
                    break;
                }
            }            
        }
    }        
}
