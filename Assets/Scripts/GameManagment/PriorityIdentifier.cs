using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityIdentifier : MonoBehaviour
{    
    [SerializeField] private RoadSpawner _roadSpawner;       
    [SerializeField] private CrossroadTrafficController _crossroadTrafficController;    
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private List<GameObject> _allCrossroad;
    [SerializeField] private List<GameObject> _currentCarsAtCrossroad;

    private CarInTraffic _currentCar;    

    private void Awake()
    {
        _roadSpawner.CreationNewPartRoad += SetAllGeneratedCrossroads;        
    }

    public void SetAllGeneratedCrossroads(GameObject crossroad)
    { 
        _allCrossroad.Add(crossroad);               
    }

    public Transform GetPositionCrossroad()
    {
        return _allCrossroad[0].transform;
    }

    public void CreateListCarsAtCrossroad()//DistanceMeter -> UnityEvent DroveCrossroad
    {
        _currentCarsAtCrossroad = new List<GameObject>(_allCrossroad[0].GetComponent<TrafficSpawner>().GetCars());
        _crossroadTrafficController.SetListCarsAtCrossroad(_currentCarsAtCrossroad);
        HighlightCurrentCar();
    }

    public void LaunchOncomingTraffic()//DistanceMeter -> UnityEvent DroveCrossroad
    {
        List <GameObject> currentOncomingTraffic = new List<GameObject>(_allCrossroad[0].GetComponent<TrafficSpawner>().GetCarsOncomingTraffic());
        foreach (var car in currentOncomingTraffic)
        {
            car.GetComponent<AIMovement>().OnMove();
        }
    }

    public void RemovePassedCrossroad()//DistanceMeter -> UnityEvent DroveCrossroad
    {
        _allCrossroad.RemoveAt(0);
        _currentCarsAtCrossroad.Clear();       
    }

    public void GivePriority()
    {
        _currentCar.SetPriorityStatus(PriorityStatus.priority);        
        PromoteCarList();
    }

    public void NotGivePriority()
    {
        _currentCar.SetPriorityStatus(PriorityStatus.nonPriority);
        PromoteCarList();
    }

    private void HighlightCurrentCar()
    {
        if (_currentCarsAtCrossroad.Count != 0)
        {
            _currentCar = _currentCarsAtCrossroad[0].GetComponent<CarInTraffic>();
            _currentCar.ShowHighlight();
        }        
    }

    private void PromoteCarList()
    {
        if (_currentCarsAtCrossroad.Count != 0)
        {            
            _currentCarsAtCrossroad.RemoveAt(0);
            HighlightCurrentCar();
        }
    }
}
