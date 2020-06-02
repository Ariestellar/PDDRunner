using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorPanel : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _timerTemp;
    [SerializeField] private int _displayTime;

    private void OnEnable()
    {
        _playerMovement.Stop();
        GameObject timer = Instantiate(_timerTemp);
        timer.GetComponent<Timer>().StartTimer(_displayTime, Hide);
    }

    private void OnDisable()
    {
        _playerMovement.OnMove();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
