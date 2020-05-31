using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TrafficSpawner 
{  
    void Init(SignsCreator signCreator, CarCreator carCreator);   
    List<GameObject> GetCars();
    List<GameObject> GetCarsOncomingTraffic();
}
