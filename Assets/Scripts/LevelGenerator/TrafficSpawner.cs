using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SignsCreator))]
[RequireComponent(typeof(CarCreator))]
public class TrafficSpawner : MonoBehaviour
{
    [SerializeField] private List<PointSpawnCar> _pointSpawnCars;    
    [SerializeField] private List<Transform> _pointSpawnSign;

    [SerializeField] private PointMovement _pointRespawnCarsEast;
    [SerializeField] private PointMovement _pointRespawnCarsNord;
    [SerializeField] private PointMovement _pointRespawnCarsWest;
    [SerializeField] private PointMovement _pointRespawnCarsPlayer;

    private SignsCreator _signCreator;
    private CarCreator _carCreator;
    private List<Car> _cars;    
    private List<SignPriorityWay> _signs;    

    private void Awake()
    {
        _signCreator = GetComponent<SignsCreator>();
        _carCreator = GetComponent<CarCreator>();
    }

    private void Start()
    {
        _signs = _signCreator.Create(_pointSpawnSign);
        SetValueSignsToSpawnPointsCars(_signs, _pointSpawnCars);
        _cars = _carCreator.Create(_pointSpawnCars);

        TestTrafficMovement();
    }

    public List<Car> GetCars()
    {
        return _cars;
    }

    public SignPriorityWay GetValueSignPlayer()
    {
        return _signs[0];
    }

    //Ссылки на точки и знаки должны быть соответственны,устанавливаются в инспекторе.(счет против часовой стрелки)
    //0 - юг(всегда игрок)
    //1 - восток
    //2 - север
    //3 - запад
    private void SetValueSignsToSpawnPointsCars(List<SignPriorityWay> signs, List<PointSpawnCar> pointSpawnCars)
    {
        for (int i = 0; i < pointSpawnCars.Count; i++)
        {
            pointSpawnCars[i].SetSignValue(signs[i + 1]);
        }
    }

    private void TestTrafficMovement()
    {
        foreach (var car in _cars)
        {
            if (car.RelativePositionCars == RelativePositionCars.east)
            {
                _pointRespawnCarsEast.GetPointsMovement(car.Direction);
            }
            else if (car.RelativePositionCars == RelativePositionCars.nord)
            {
                _pointRespawnCarsNord.GetPointsMovement(car.Direction);
            }
            else if (car.RelativePositionCars == RelativePositionCars.west)
            {
                _pointRespawnCarsWest.GetPointsMovement(car.Direction);
            }
            else if (car.RelativePositionCars == RelativePositionCars.player)
            {
                _pointRespawnCarsPlayer.GetPointsMovement(car.Direction);
            }
        }
    }
}
