using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Класс описывает момент когда автомобиль игрока приближается к ключевому элементу дороги и расставляет приоритеты 
public class PriorityIdentifier : MonoBehaviour
{    
    [SerializeField] private RoadSpawner _roadSpawner;           
    [SerializeField] private List<GameObject> _allCrossroad;
    [SerializeField] private List<Car> _currentCarsAtCrossroad;
    [SerializeField] private SignPriorityWay _valueSignPlayer;
    //[SerializeField] private List<RelativePositionCars> _sequenceMovementCarsFirstQueue;
    //[SerializeField] private List<RelativePositionCars> _sequenceMovementCarsSecondQueue;

    private Car _currentCar;    

    private void Awake()
    {
        _roadSpawner.CreationNewPartRoad += SetAllGeneratedCrossroads;        
    }

    //Новые элементы дороги включаются в конец списока при создании(т.к. сначала списка прекрестки используются и удалаются)
    public void SetAllGeneratedCrossroads(GameObject crossroad)
    { 
        _allCrossroad.Add(crossroad);               
    }

    public Transform GetPositionCrossroad()
    {
        return _allCrossroad[0].transform;
    }

    //При проследовании последнего перекрестка, получаем новый, предстоящий перекресток и список актуальных машин сгенерированных на нем
    public void CreateListCarsAtCrossroad()//DistanceMeter -> UnityEvent DroveCrossroad 
    {
        RemovePassedCrossroad();//Первоочередно, старый перекресток удаляем из начала списка, и очищаем список машин от пройденных
        _currentCarsAtCrossroad = new List<Car>(_allCrossroad[0].GetComponent<TrafficSpawner>().GetCars());
        _valueSignPlayer = _allCrossroad[0].GetComponent<TrafficSpawner>().GetValueSignPlayer();        
        HighlightCurrentCar();        
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
    //Задаем правильные(эталонные приоритеты)
    private void SetTruePriorities()
    {
        
    }
    
    private void RemovePassedCrossroad()
    {
        _allCrossroad.RemoveAt(0);
        _currentCarsAtCrossroad.Clear();
    }

    private void HighlightCurrentCar()
    {
        if (_currentCarsAtCrossroad.Count != 0)
        {
            _currentCar = _currentCarsAtCrossroad[0].GetComponent<Car>();
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
