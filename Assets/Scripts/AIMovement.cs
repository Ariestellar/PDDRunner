using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField] public List<Transform> _points;
    [SerializeField] private int _destPoint = 0;
    [SerializeField] private NavMeshAgent _agent;

    private void Update()
    {
        if (_agent.remainingDistance < 0.05f)
            GotoNextPoint();
    }

    private void GotoNextPoint()
    {        
        if (_points.Count == 0)
            return;
        
        _agent.destination = _points[_destPoint].position;
        _destPoint = (_destPoint + 1) % _points.Count;
    }
}
