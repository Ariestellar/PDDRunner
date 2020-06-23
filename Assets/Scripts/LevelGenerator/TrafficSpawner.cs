using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SignsCreator))]
[RequireComponent(typeof(CarCreator))]
public class TrafficSpawner : MonoBehaviour
{
    [SerializeField] private MovementTraffic _movementTraffic;
    [SerializeField] private List<PointSpawnCar> _pointSpawnCars;    
    [SerializeField] private List<Transform> _pointSpawnSign;

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

        _movementTraffic.Go(_cars); //пока запускаю здесь для тестовой сцены потом перенести в событие, когда машина игрока достигает перекрестка
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
            pointSpawnCars[i].SetSignValue(signs[i]);
        }
    }    
}
