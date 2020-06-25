using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private RoadSpawner _roadSpawner;
    [SerializeField] private PlayerGeneration _playerGeneration;
    private int _roadsCount = 4;//колличество загруженных перекрестков при старте (количество постоянно находящихся на уровне перекрестков впереди машины равно 3 и задаеться с помощью вычисления расстояния до последнего в "DistanceMeter")

    void Start()
    {
        StartSpawnRoad();
        _playerGeneration.Create();
    }

    private void StartSpawnRoad()
    {
        for (int i = 0; i < _roadsCount; i++)
            _roadSpawner.SpawnPartRoad();
    }
}
