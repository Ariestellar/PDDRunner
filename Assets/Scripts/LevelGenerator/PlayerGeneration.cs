using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneration : MonoBehaviour
{
    //Дублирование кода, временное решение, отрефакторить, уже есть фабрика CarCreator
    //Отрефакторить паттерном фабрика
    //Пока включенно для примера, генеряться разные машины разных цветов.
    [SerializeField] private List<GameObject> _templateCars;
    [SerializeField] private List<Material> _colorCars;
    [SerializeField] private Transform _playerPosition;
    private Car _playerCar;

    public void Create()
    {
        GameObject currentCar = Instantiate(_templateCars[Random.Range(0, _templateCars.Count)], _playerPosition);
        
        currentCar.GetComponentInChildren<MeshRenderer>().material = SetColorCar();
        Car car = currentCar.GetComponent<Car>();
        car.Init(VehicleDirection.straight, RelativePositionCars.player, _playerPosition.GetComponent<PointSpawnCar>().signValue);
        _playerCar = car;
    }

    public Car GetPlayerCar()
    {
        return _playerCar;
    }

    private Material SetColorCar()
    {
        return _colorCars[Random.Range(0, _colorCars.Count)];
    }
}
