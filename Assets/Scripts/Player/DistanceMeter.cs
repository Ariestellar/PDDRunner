using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PriorityIdentifier))]
public class DistanceMeter : MonoBehaviour
{
    [SerializeField] private RoadSpawner _roadSpawner;    
    [SerializeField] private UnityEvent _reachingEndRoad;
    [SerializeField] private UnityEvent _droveCrossroad;
    [SerializeField] private UnityEvent _startTrafficAtCrossroad;
    [SerializeField] private float _distanceToСreationNewPartRoad;
    [SerializeField] private float _distanceToDroveCrossroad;
    [SerializeField] private float _distanceToStartTrafficCrossroad;
    [SerializeField] private bool _isCrossroadsReached;

    private PriorityIdentifier _priorityIdentifier;

    private void Awake()
    {
        _priorityIdentifier = GetComponent<PriorityIdentifier>();
    }

    private void FixedUpdate()
    {        
        DistanceCheckForSpawn();
        DistanceCheckAfterDroveCrossroad(_priorityIdentifier.GetPositionCrossroad());
        DistanceCheckStartTrafficCrossroad(_priorityIdentifier.GetPositionCrossroad());
    }
    //Проверка дистанции до последнего участка дороги что бы сгенерировать новый
    private void DistanceCheckForSpawn()
    {
        if (_roadSpawner.GetLastPartRoad().position.z - transform.position.z < _distanceToСreationNewPartRoad)
        {
            _reachingEndRoad?.Invoke();
            Debug.Log("Сгенерировать еще элемент дороги");
        }
    }
    //Проверка дистанции до начала регулирования движения на текущем перекрестке
    private void DistanceCheckStartTrafficCrossroad(Transform currentCrossroad)
    {        
        if (_isCrossroadsReached == false && currentCrossroad != null && transform.position.z - currentCrossroad.position.z  > _distanceToStartTrafficCrossroad)
        {
            _isCrossroadsReached = true;
            _startTrafficAtCrossroad?.Invoke();            
            Debug.Log("Машины на элементе дороги поехали");
        }
    }

    //Проверка дистанции до начала интерактива на следующем перекрестке
    private void DistanceCheckAfterDroveCrossroad(Transform currentCrossroad)
    {
        if (currentCrossroad != null && transform.position.z - currentCrossroad.position.z > _distanceToDroveCrossroad)
        {
            _isCrossroadsReached = false;
            _droveCrossroad?.Invoke();
            Debug.Log("Интерактив машин на пэлементе дороги включен");
        }
    }

}
