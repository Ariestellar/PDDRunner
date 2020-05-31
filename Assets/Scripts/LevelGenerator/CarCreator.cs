using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCreator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _templateCars;
    [SerializeField] private List<Material> _colorCars;

    public List<GameObject> Create(List<Transform> pointSpawnCars, List<SignPriorityWay> signPriorityWays)
    {
        List<GameObject> cars = new List<GameObject>();
        List<Transform> generatePointSpawnCars = GetGenerateCurrentPointSpawn(pointSpawnCars);

        for (int i = 0; i < generatePointSpawnCars.Count; i++)
        {
            GameObject car = CreateCar(generatePointSpawnCars[i]);
            SetSequeceCar(car, signPriorityWays);
            cars.Add(car);
        }
        return cars;
    }

    public List<GameObject> Create(List<Transform> _passingTraffic, List<Transform> _oncomingTraffic)
    {
        List<GameObject> cars = new List<GameObject>();
        List<Transform> generatePassingTraffic = GetGenerateCurrentPointSpawn(_passingTraffic);

        for (int i = 0; i < generatePassingTraffic.Count; i++)
        {
            GameObject car = CreateCar(generatePassingTraffic[i]);
        }

        for (int i = 0; i < _oncomingTraffic.Count; i++)
        {
            GameObject car = CreateCar(_oncomingTraffic[i]);            
            cars.Add(car);
        }
        return cars;
    }

    public GameObject CreateCar(Transform positionCar)
    {        
        GameObject car = Instantiate(_templateCars[Random.Range(0, _templateCars.Count)], positionCar);      
        car.GetComponentInChildren<MeshRenderer>().material = SetColorCar();        
        return car;
    }

    private List<Transform> GetGenerateCurrentPointSpawn(List<Transform> pointSpawnCarsPosition)
    {
        int countCars = Random.Range(1, 4);
        List<Transform> currentPointSpawnCars = new List<Transform>();
        Transform pointSpawnCars;
        for (int i = 0; i < countCars; i++)
        {
            do
            {
                pointSpawnCars = pointSpawnCarsPosition[Random.Range(0, pointSpawnCarsPosition.Count)];
            } while (currentPointSpawnCars.Contains(pointSpawnCars));

            currentPointSpawnCars.Add(pointSpawnCars);
        }
        return currentPointSpawnCars;
    }

    private Material SetColorCar()
    {
        return _colorCars[Random.Range(0, _colorCars.Count)];
    }

    private void SetSequeceCar(GameObject car, List<SignPriorityWay> signPriorityWays)
    {
        CarInTraffic carInTraffic = car.GetComponent<CarInTraffic>();

        VehicleDirection vehicleDirection = GetRandomDirection();

        /*carInTraffic.EnableTurnSignalCar(vehicleDirection);
        carInTraffic.SetVehicleDirection(vehicleDirection);*/

        //Временное решение "в лоб", отрефакторить, формализировать относительное положение машин
        if (car.transform.rotation.eulerAngles.y == 180)//Машина справа
        {
            if ((int)signPriorityWays[1] == 0 || (int)signPriorityWays[1] == 1 || ((int)signPriorityWays[1] > 2 && (int)signPriorityWays[1] < 5) || (int)signPriorityWays[1] == 6)
            {
                carInTraffic.SetSequenceCars(1);
                vehicleDirection = VehicleDirection.straight;//временно 
            }
            else
            {
                carInTraffic.SetSequenceCars(0);
            }            
        }
        else if(car.transform.rotation.eulerAngles.y == 90)//Машина напротив
        {
            carInTraffic.SetSequenceCars(0);
        }
        else if (car.transform.rotation.eulerAngles.y == 0)//Машина слева
        {
            if ((int)signPriorityWays[3] == 1 || (int)signPriorityWays[3] == 4)
            {
                carInTraffic.SetSequenceCars(1);
            }
            else
            {
                carInTraffic.SetSequenceCars(0);
            }
        }

        carInTraffic.EnableTurnSignalCar(vehicleDirection);
        carInTraffic.SetVehicleDirection(vehicleDirection);
    }


    private VehicleDirection GetRandomDirection()
    {        
        int directionNumber = Random.Range(0,3);
        return (VehicleDirection)directionNumber;
    }
}
