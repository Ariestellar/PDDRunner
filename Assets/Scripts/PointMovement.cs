using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PointMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private bool _isMove;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_isMove)
        {
            if (_agent.remainingDistance < 0.05f)
                GotoNextPoint();
        }        
    }

    private void GotoNextPoint()
    {
        if (_points.Count == 0)
        {            
            Destroy(this.gameObject);
        }
        else
        {
            _agent.destination = _points[0].position;
            _points.RemoveAt(0);
        } 
    }

    public void SetPointsMovement(List<Transform> points)
    {
        _points = points;
    }

    public void SetMove(bool isMove)
    {
        _isMove = isMove;   
    }
}
