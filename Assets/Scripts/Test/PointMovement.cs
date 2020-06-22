using GameParametrs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PointMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _pointsMovementRight;
    [SerializeField] private List<Transform> _pointsMovementStraight;
    [SerializeField] private List<Transform> _pointsMovementLeft;    
    [SerializeField] private List<GameObject> _sidesMovement;
        
    private List<List<Transform>> _pointsMovement;

    private void Start()
    {
        _pointsMovement = new List<List<Transform>>() { _pointsMovementLeft, _pointsMovementRight, _pointsMovementStraight };
    }

    public void GetPointsMovement(VehicleDirection vehicleDirection)
    {
        _sidesMovement[(int)vehicleDirection].SetActive(true);
        List<Transform>  currentPoint = _pointsMovement[(int)vehicleDirection];        

        /*
         * LineRenderer для рисования направлений поворота машин. Хотел использовать для нахождения пересекающихся маршрутов.
         * Неактуальна для меня, так как машины ездят по предсказуемым маршрутам и достаточно следить за несколькими точками 
         * пересечения, чем за целой линией. Возможно будет актуально для упрощения режима игры.
         * 
        Vector3[] valuePoint = new Vector3[currentPoint.Count];
        for (int i = 0; i < currentPoint.Count; i++)
        {
            valuePoint[i] = currentPoint[i].position;
        }
        _valuePointTest = valuePoint;        
        _lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        _lineRenderer.positionCount = valuePoint.Length;
        _lineRenderer.generateLightingData = true;
        _lineRenderer.material = _material;
        _lineRenderer.SetPositions(valuePoint);
        */
    }
}
