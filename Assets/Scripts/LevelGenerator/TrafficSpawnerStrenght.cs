using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSpawnerStrenght : MonoBehaviour, TrafficSpawner
{
    [SerializeField] private List<GameObject> _cars;
    [SerializeField] private List<GameObject> _oncomingCars;
    [SerializeField] private List<Transform> _oncomingTraffic;
    [SerializeField] private List<Transform> _passingTraffic;

    public void Init(SignsCreator signCreator, CarCreator carCreator)
    {
        _oncomingCars = carCreator.Create(_passingTraffic, _oncomingTraffic);        
    }

    public List<GameObject> GetCarsOncomingTraffic()
    {
        return _oncomingCars;
    }

    public List<GameObject> GetCars()
    {
        return _cars;
    }
}
