using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SignsCreator))]
[RequireComponent(typeof(CarCreator))]
public class TrafficSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _pointSpawnCars;    
    [SerializeField] private List<Transform> _pointSpawnSign;

    private SignsCreator _signCreator;
    private CarCreator _carCreator;
    private List<GameObject> _cars;

    private void Awake()
    {
        _signCreator = GetComponent<SignsCreator>();
        _carCreator = GetComponent<CarCreator>();
    }

    private void Start()
    {
        _cars = _carCreator.Create(_pointSpawnCars, _signCreator.Create(_pointSpawnSign));
    }

    public List<GameObject> GetCars()
    {
        return _cars;
    }
}
