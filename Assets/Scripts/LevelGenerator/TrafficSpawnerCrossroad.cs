using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSpawnerCrossroad : MonoBehaviour, TrafficSpawner
{
    [SerializeField] private List<Transform> _pointSpawnCars;    
    [SerializeField] private List<Transform> _pointSpawnSign;
    [SerializeField] private List<GameObject> _oncomingCars;
    [SerializeField] private List<GameObject> _cars; 

    public void Init(SignsCreator signCreator, CarCreator carCreator)
    {
        signCreator.Create(_pointSpawnSign);        
        _cars = carCreator.Create(_pointSpawnCars, signCreator.ArrangementValuesSigns);        
    }

    public List<GameObject> GetCars()
    {
        return _cars;
    }

    public List<GameObject> GetCarsOncomingTraffic()
    {
        return _oncomingCars;
    }    
}
