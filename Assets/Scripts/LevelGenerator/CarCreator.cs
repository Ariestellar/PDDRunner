using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCreator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _templateCars;    
    [SerializeField] private List<Material> _colorCars;

    public List<Car> Create(List<PointSpawnCar> pointSpawnCars)
    {        
        List<Car> cars = new List<Car>();

        foreach (var pointSpawn in pointSpawnCars)
        {
            if (pointSpawn.spawnChanceOnPoint >= Random.Range(0, 100))
            {                
                cars.Add(CreateCar(pointSpawn));
            }
        }
        return cars;
    }

    private Car CreateCar(PointSpawnCar positionCar)
    {        
        GameObject currentCar = Instantiate(_templateCars[Random.Range(0, _templateCars.Count)], positionCar.transform);
        currentCar.GetComponentInChildren<MeshRenderer>().material = SetColorCar();
        Car car = currentCar.GetComponent<Car>();
        car.Init(positionCar.GetCarDirection(), positionCar.relativePositionCars, positionCar.signValue);
        return car;
    }

    private Material SetColorCar()
    {
        return _colorCars[Random.Range(0, _colorCars.Count)];
    }
}
