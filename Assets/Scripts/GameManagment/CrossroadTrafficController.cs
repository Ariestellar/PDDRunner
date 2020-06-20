using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadTrafficController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;    
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _timerTemp;
    [SerializeField] private List<GameObject> _carsAtCrossroad;

    private GameObject _timer;

    private void Start()
    {
        _timer = Instantiate(_timerTemp, this.gameObject.transform);
    }

    public void SetListCarsAtCrossroad(List<GameObject> carsAtCrossroad)
    {
        _carsAtCrossroad = new List<GameObject>(carsAtCrossroad);
    }

    public void StartMoveAtCrossroad()//DistanceMeter -> UnityEvent StartTrafficAtCrossroad
    {
        foreach (var car in _carsAtCrossroad)
        {
            if (car.GetComponent<Car>().PriorityStatus == PriorityStatus.priority)
            {
                _playerData.IncreaseCountActionsGiveWay();
            }
            //Реализация пока без конкретной очередности(для равнозначных дорог)
            if (car.GetComponent<Car>().SequenceCars > 0)
            {                
                //Машина справа имеет 1(первая) начинает движение при приближении игрока к перекрестку
                car.GetComponent<AIMovement>().OnMove();
            }
        }
        MovePlayerCarAtCrossroad();
    }

    private void MovePlayerCarAtCrossroad()
    {
        if (_playerData.CountActionsGiveWay > 0)
        {
            _playerMovement.Stop();            
            _timer.GetComponent<Timer>().StartTimer(_playerData.CountActionsGiveWay, _playerMovement.OnMove);
            //Количество заданных приоритетов равно число секунд стоянки у перекрестка
        }
    }
}
