using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler : MonoBehaviour
{    
    [SerializeField] private UnityEvent _сlashWithCar;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<CarInTraffic>())
        {
            _сlashWithCar?.Invoke();
        }
    }    
}
