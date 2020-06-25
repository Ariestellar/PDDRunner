using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoadSpawner : MonoBehaviour
{
    public UnityAction<GameObject> CreationNewPartRoad;

    [SerializeField] private GameObject[] _roadPrefabs;
    [SerializeField] private GameObject _startBlock;    
    [SerializeField] private List<GameObject> _currentPartsRoads;

    private float _roadLength = 50;

    public List<GameObject> CurrentPartsRoads => _currentPartsRoads;
    
    public Transform GetLastPartRoad()
    {
        return CurrentPartsRoads[CurrentPartsRoads.Count - 1].transform;
    }

    public void SpawnPartRoad()//DistanceMeter -> UnityEvent ReachingEndRoad
    {        
        GameObject currentPartRoad = Instantiate(_roadPrefabs[Random.Range(0, _roadPrefabs.Length)], transform);
        Vector3 roadPositionCurrentPart;

        if (CurrentPartsRoads.Count > 0)
            roadPositionCurrentPart = CurrentPartsRoads[CurrentPartsRoads.Count - 1].transform.position + new Vector3(0, 0, _roadLength);
        else
            roadPositionCurrentPart = new Vector3(_startBlock.transform.position.x, _startBlock.transform.position.y, _startBlock.transform.position.z + _roadLength);

        currentPartRoad.transform.position = roadPositionCurrentPart;

        CurrentPartsRoads.Add(currentPartRoad);
        CreationNewPartRoad?.Invoke(currentPartRoad);
    }

    public void DestroyRoad()//DistanceMeter -> UnityEvent ReachingEndRoad
    {
        Destroy(CurrentPartsRoads[0]);
        CurrentPartsRoads.RemoveAt(0);
    }
}
