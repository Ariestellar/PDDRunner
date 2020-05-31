using GameParametrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backlight : MonoBehaviour
{
    [SerializeField] private Sprite _signMove; 
    [SerializeField] private Sprite _signStop;
    [SerializeField] private GameObject _signCar;
    [SerializeField] private GameObject _highliteCar;
    [SerializeField] private GameObject _arrow;

    public void Show(bool value)
    {
        _highliteCar.SetActive(value);
        _arrow.SetActive(value);
    }

    public void ChangeSignMoveCar(PriorityStatus priority)
    {
        Show(false);
        _signCar.SetActive(true);
        if (priority == PriorityStatus.priority)
        {
            _signCar.GetComponent<SpriteRenderer>().sprite = _signMove;
        }
        else 
        {
            _signCar.GetComponent<SpriteRenderer>().sprite = _signStop;
        }        
    }
}
