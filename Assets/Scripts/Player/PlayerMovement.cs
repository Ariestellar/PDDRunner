using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PriorityIdentifier))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedChangeLane;    
    [SerializeField] private GameObject _timerTemp; 

    private PriorityIdentifier _priorityIndentifier;
    private Vector3 _directionMove;
    private GameObject _timer;
    private float _timerChargeDefault;
    private bool _isMove = true;      
    private int _directionChangeLane;
    private int _laneNumber = 1;
    private int _laneCount = 1;
    private float _firstLanePos = -2.75f;
    private float _laneDistance = 2.75f;
    
    private void Start()
    {
        SwipeController.SwipeEvent += CheckInput;
        _priorityIndentifier = GetComponent<PriorityIdentifier>();
        _timer = Instantiate(_timerTemp, this.gameObject.transform);
        _directionMove = Vector3.forward;
    }

    public void Stop()
    {
        _isMove = false;
    }

    public void OnMove()
    {
        _isMove = true;
    }

    private void FixedUpdate()
    {
        Move();        
    }

    private void Move()
    {
        if (_isMove)
        {            
            transform.Translate(_directionMove * _speed * Time.fixedDeltaTime);
            LineСhange();
        }        
    }

    private void LineСhange()
    {
        Vector3 newPositionInLane = transform.position;
        newPositionInLane.x = Mathf.Lerp(newPositionInLane.x, _firstLanePos + (_laneNumber * _laneDistance), Time.fixedDeltaTime * _speedChangeLane);
        transform.position = newPositionInLane;
    }

    private void CheckInput(SwipeController.SwipeType type)
    {        
        if (type == SwipeController.SwipeType.RIGHT)
        {            
            _directionChangeLane = 1;
        } 
        else if (type == SwipeController.SwipeType.LEFT)
        {            
            _directionChangeLane = -1;
        }

        _laneNumber += _directionChangeLane;
        _laneNumber = Mathf.Clamp(_laneNumber, 0, _laneCount);

        if (type == SwipeController.SwipeType.UP)
        {
            _priorityIndentifier.NotGivePriority();
        }
        else if (type == SwipeController.SwipeType.DOWN)
        {
            _priorityIndentifier.GivePriority();
        }
    }          
}
