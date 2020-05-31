using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private Action _countdownStopped;
    private Action<int, float> _tick;
    private Coroutine _timerRoutine;
    private float _pastTime;

    public event Action CountdownStopped
    {
        add => _countdownStopped += value;
        remove => _countdownStopped += value;
    }

    public event Action<int, float> Tick
    {
        add => _tick += value;
        remove => _tick += value;
    }

    public void StartTimer(float duration, Action onEnd = null)
    {
        if (_timerRoutine != null)
            StopCoroutine(_timerRoutine);

        _timerRoutine = StartCoroutine(Run(duration, onEnd));
    }

    public float StopTimer()
    {
        if (_timerRoutine != null)
            StopCoroutine(_timerRoutine);

        _countdownStopped?.Invoke();
        return _pastTime;
    }

    private IEnumerator Run(float duration, Action onEnd)
    {
        _pastTime = 0.0f;
        while (_pastTime < duration)
        {
            _pastTime += Time.deltaTime;
            _tick?.Invoke(Convert.ToInt32(_pastTime), duration);

            yield return null;
        }

        onEnd?.Invoke();
    }
}
