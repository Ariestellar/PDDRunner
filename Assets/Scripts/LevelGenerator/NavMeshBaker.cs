using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    [SerializeField] private NavMeshSurface[] _navMeshs;    
    void Awake()
    {
        _navMeshs = GetComponentsInChildren<NavMeshSurface>();
        foreach (var navMesh in _navMeshs)
        {
            navMesh.BuildNavMesh();
        }
    }    
}
